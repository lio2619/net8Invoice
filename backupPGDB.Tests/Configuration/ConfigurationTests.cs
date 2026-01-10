using backupPGDB.Configuration;

namespace backupPGDB.Tests.Configuration;

/// <summary>
/// 設定類別單元測試
/// </summary>
[TestClass]
public class ConfigurationTests
{
    /// <summary>
    /// 測試：BackupSettings 應正確產生備份檔案路徑
    /// </summary>
    [TestMethod]
    public void BackupSettings_GenerateBackupFilePath_ReturnsCorrectFormat()
    {
        // Arrange
        var settings = new BackupSettings
        {
            TempDirectory = @"C:\Temp\Backups",
            FileNamePrefix = "invoicing"
        };

        // Act
        var path = settings.GenerateBackupFilePath();

        // Assert
        Assert.IsTrue(path.StartsWith(@"C:\Temp\Backups\invoicing_"));
        Assert.IsTrue(path.EndsWith(".backup"));
        
        // 驗證檔名格式包含日期時間 (yyyyMMdd_HHmmss)
        var fileName = Path.GetFileNameWithoutExtension(path);
        // 格式: invoicing_yyyyMMdd_HHmmss = invoicing_ (10) + yyyyMMdd_HHmmss (15) = 25
        Assert.AreEqual(25, fileName.Length);
    }

    /// <summary>
    /// 測試：CloudStorageSettings 應正確設定 Endpoint
    /// </summary>
    [TestMethod]
    public void CloudStorageSettings_Endpoint_CanBeSet()
    {
        // Arrange
        var settings = new CloudStorageSettings
        {
            Endpoint = "https://abc123def456.r2.cloudflarestorage.com"
        };

        // Assert
        Assert.AreEqual("https://abc123def456.r2.cloudflarestorage.com", settings.Endpoint);
    }

    /// <summary>
    /// 測試：DatabaseSettings 應有正確的預設值
    /// </summary>
    [TestMethod]
    public void DatabaseSettings_HasCorrectDefaultValues()
    {
        // Arrange & Act
        var settings = new DatabaseSettings();

        // Assert
        Assert.AreEqual("localhost", settings.Host);
        Assert.AreEqual(5432, settings.Port);
        Assert.AreEqual("postgres", settings.Username);
        Assert.AreEqual(@"C:\Program Files\PostgreSQL\17\bin\pg_dump.exe", settings.PgDumpPath);
    }

    /// <summary>
    /// 測試：BackupSettings 的 RetainLocalFileAfterUpload 預設為 false
    /// </summary>
    [TestMethod]
    public void BackupSettings_RetainLocalFileAfterUpload_DefaultsToFalse()
    {
        // Arrange & Act
        var settings = new BackupSettings();

        // Assert
        Assert.IsFalse(settings.RetainLocalFileAfterUpload);
    }

    /// <summary>
    /// 測試：CloudStorageSettings 的 ForcePathStyle 預設為 true (R2 必要)
    /// </summary>
    [TestMethod]
    public void CloudStorageSettings_ForcePathStyle_DefaultsToTrue()
    {
        // Arrange & Act
        var settings = new CloudStorageSettings();

        // Assert
        Assert.IsTrue(settings.ForcePathStyle);
    }
}
