using backupPGDB.Configuration;
using backupPGDB.Interfaces;
using backupPGDB.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace backupPGDB;

/// <summary>
/// 每日備份工具主程式
/// 用途：備份 PostgreSQL 資料庫至 Cloudflare R2
/// 執行方式：由 Windows Task Scheduler 定時觸發
/// </summary>
public class Program
{
    public static async Task<int> Main(string[] args)
    {
        try
        {
            // 建立 Host 並設定 DI 容器
            using var host = CreateHostBuilder(args).Build();
            
            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("=== PostgreSQL 備份工具啟動 ===");
            logger.LogInformation("執行時間: {DateTime}", DateTime.Now);

            // 取得備份服務並執行
            var backupService = host.Services.GetRequiredService<IBackupService>();
            await backupService.ExecuteAsync();

            logger.LogInformation("=== 備份工具結束 ===");
            return 0; // 成功
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"備份工具執行失敗: {ex.Message}");
            Console.Error.WriteLine(ex.StackTrace);
            return 1; // 失敗
        }
    }

    /// <summary>
    /// 建立並設定 IHostBuilder
    /// </summary>
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                // 載入設定檔
                config.SetBasePath(AppContext.BaseDirectory)
                      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                      .AddJsonFile("appsettings.Secret.json", optional: false, reloadOnChange: false);
            })
            .ConfigureServices((context, services) =>
            {
                // 註冊強型別設定
                services.Configure<BackupSettings>(
                    context.Configuration.GetSection("Backup"));
                services.Configure<DatabaseSettings>(
                    context.Configuration.GetSection("Database"));
                services.Configure<CloudStorageSettings>(
                    context.Configuration.GetSection("CloudStorage"));

                // 註冊服務 (使用 Interface 進行依賴注入)
                services.AddSingleton<IProcessRunner, ProcessRunner>();
                services.AddSingleton<IDatabaseDumper, PostgresDumper>();
                services.AddSingleton<ICloudStorageService, R2StorageService>();
                services.AddSingleton<IBackupService, BackupService>();
            })
            .ConfigureLogging((context, logging) =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                
                // 可選：在生產環境中加入檔案日誌
                // logging.AddFile("Logs/backup-{Date}.log");
            });
}
