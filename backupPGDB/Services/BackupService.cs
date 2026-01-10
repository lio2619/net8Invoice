using backupPGDB.Configuration;
using backupPGDB.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace backupPGDB.Services;

/// <summary>
/// 備份服務
/// 協調整個備份流程：資料庫匯出 -> 雲端上傳 -> 清理暫存
/// </summary>
public sealed class BackupService : IBackupService
{
    private readonly IDatabaseDumper _databaseDumper;
    private readonly ICloudStorageService _cloudStorageService;
    private readonly BackupSettings _backupSettings;
    private readonly ILogger<BackupService> _logger;

    public BackupService(
        IDatabaseDumper databaseDumper,
        ICloudStorageService cloudStorageService,
        IOptions<BackupSettings> backupSettings,
        ILogger<BackupService> logger)
    {
        _databaseDumper = databaseDumper;
        _cloudStorageService = cloudStorageService;
        _backupSettings = backupSettings.Value;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("=== 開始執行每日備份流程 ===");
        var startTime = DateTime.Now;
        string? backupFilePath = null;

        try
        {
            // 步驟 1: 執行資料庫匯出
            _logger.LogInformation("步驟 1/3: 執行資料庫匯出...");
            backupFilePath = await _databaseDumper.DumpAsync(cancellationToken);

            var fileInfo = new FileInfo(backupFilePath);
            _logger.LogInformation("備份檔案大小: {Size:N2} MB", fileInfo.Length / 1024.0 / 1024.0);

            // 步驟 2: 上傳至雲端儲存
            _logger.LogInformation("步驟 2/3: 上傳至雲端儲存...");
            var remoteKey = Path.GetFileName(backupFilePath);
            await _cloudStorageService.UploadFileAsync(backupFilePath, remoteKey, cancellationToken);

            // 步驟 3: 清理本地暫存檔案
            _logger.LogInformation("步驟 3/3: 清理本地暫存...");
            if (!_backupSettings.RetainLocalFileAfterUpload && File.Exists(backupFilePath))
            {
                File.Delete(backupFilePath);
                _logger.LogInformation("已刪除本地備份檔案: {BackupFilePath}", backupFilePath);
            }
            else
            {
                _logger.LogInformation("保留本地備份檔案: {BackupFilePath}", backupFilePath);
            }

            var duration = DateTime.Now - startTime;
            _logger.LogInformation("=== 備份流程完成，耗時: {Duration:mm\\:ss} ===", duration);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "備份流程發生錯誤");

            // 如果上傳失敗，保留本地備份檔案以便手動處理
            if (backupFilePath != null && File.Exists(backupFilePath))
            {
                _logger.LogWarning("保留本地備份檔案以便手動處理: {BackupFilePath}", backupFilePath);
            }

            throw;
        }
    }
}
