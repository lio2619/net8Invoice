using backupPGDB.Configuration;
using backupPGDB.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace backupPGDB.Services;

/// <summary>
/// PostgreSQL 資料庫匯出服務
/// 使用 pg_dump.exe 產生 .backup 檔案
/// </summary>
public sealed class PostgresDumper : IDatabaseDumper
{
    private readonly IProcessRunner _processRunner;
    private readonly DatabaseSettings _databaseSettings;
    private readonly BackupSettings _backupSettings;
    private readonly ILogger<PostgresDumper> _logger;

    public PostgresDumper(
        IProcessRunner processRunner,
        IOptions<DatabaseSettings> databaseSettings,
        IOptions<BackupSettings> backupSettings,
        ILogger<PostgresDumper> logger)
    {
        _processRunner = processRunner;
        _databaseSettings = databaseSettings.Value;
        _backupSettings = backupSettings.Value;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<string> DumpAsync(CancellationToken cancellationToken = default)
    {
        // 確保暫存目錄存在
        if (!Directory.Exists(_backupSettings.TempDirectory))
        {
            Directory.CreateDirectory(_backupSettings.TempDirectory);
            _logger.LogInformation("已建立暫存目錄: {TempDirectory}", _backupSettings.TempDirectory);
        }

        var outputPath = _backupSettings.GenerateBackupFilePath();
        
        // 組合 pg_dump 參數
        // 使用 custom 格式 (-Fc) 產生壓縮的備份檔案
        var arguments = string.Join(" ",
            $"-h {_databaseSettings.Host}",
            $"-p {_databaseSettings.Port}",
            $"-U {_databaseSettings.Username}",
            $"-d {_databaseSettings.DatabaseName}",
            "-Fc", // Custom format (壓縮)
            $"-f \"{outputPath}\"");

        _logger.LogInformation("開始執行 pg_dump...");
        _logger.LogDebug("pg_dump 路徑: {PgDumpPath}", _databaseSettings.PgDumpPath);
        _logger.LogDebug("輸出檔案: {OutputPath}", outputPath);

        // 透過環境變數傳遞密碼 (避免在命令列中暴露)
        var environmentVariables = new Dictionary<string, string>
        {
            ["PGPASSWORD"] = _databaseSettings.Password
        };

        var result = await _processRunner.RunAsync(
            _databaseSettings.PgDumpPath,
            arguments,
            environmentVariables,
            cancellationToken);

        if (result.ExitCode != 0)
        {
            _logger.LogError("pg_dump 執行失敗 (Exit Code: {ExitCode})", result.ExitCode);
            _logger.LogError("錯誤訊息: {StandardError}", result.StandardError);
            
            throw new InvalidOperationException(
                $"pg_dump 執行失敗 (Exit Code: {result.ExitCode}): {result.StandardError}");
        }

        _logger.LogInformation("pg_dump 執行成功，備份檔案: {OutputPath}", outputPath);
        return outputPath;
    }
}
