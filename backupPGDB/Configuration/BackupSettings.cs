namespace backupPGDB.Configuration;

/// <summary>
/// 備份流程設定 (Strongly Typed Configuration)
/// </summary>
public sealed class BackupSettings
{
    /// <summary>
    /// 備份檔案暫存目錄
    /// </summary>
    public string TempDirectory { get; set; } = @"C:\Temp\PgBackups";

    /// <summary>
    /// 備份檔案名稱前綴
    /// </summary>
    public string FileNamePrefix { get; set; } = "invoicing";

    /// <summary>
    /// 上傳後是否保留本地檔案
    /// </summary>
    public bool RetainLocalFileAfterUpload { get; set; } = false;

    /// <summary>
    /// 產生備份檔案完整路徑
    /// </summary>
    public string GenerateBackupFilePath()
    {
        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        var fileName = $"{FileNamePrefix}_{timestamp}.backup";
        return Path.Combine(TempDirectory, fileName);
    }
}
