using backupPGDB.Configuration;
using backupPGDB.Interfaces;
using backupPGDB.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace backupPGDB.Tests.Services;

/// <summary>
/// BackupService 單元測試
/// </summary>
[TestClass]
public class BackupServiceTests
{
    private Mock<IDatabaseDumper> _mockDatabaseDumper = null!;
    private Mock<ICloudStorageService> _mockCloudStorageService = null!;
    private Mock<ILogger<BackupService>> _mockLogger = null!;
    private IOptions<BackupSettings> _backupSettings = null!;
    private BackupService _backupService = null!;
    private string _testTempDir = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockDatabaseDumper = new Mock<IDatabaseDumper>();
        _mockCloudStorageService = new Mock<ICloudStorageService>();
        _mockLogger = new Mock<ILogger<BackupService>>();
        
        // 使用實際的暫存目錄，避免 FileInfo 例外
        _testTempDir = Path.Combine(Path.GetTempPath(), "backupPGDB_Tests");
        
        _backupSettings = Options.Create(new BackupSettings
        {
            TempDirectory = _testTempDir,
            FileNamePrefix = "test",
            RetainLocalFileAfterUpload = false
        });

        _backupService = new BackupService(
            _mockDatabaseDumper.Object,
            _mockCloudStorageService.Object,
            _backupSettings,
            _mockLogger.Object);
    }

    [TestCleanup]
    public void Cleanup()
    {
        // 清理測試暫存目錄
        if (Directory.Exists(_testTempDir))
        {
            try
            {
                Directory.Delete(_testTempDir, true);
            }
            catch
            {
                // 忽略清理錯誤
            }
        }
    }

    /// <summary>
    /// 測試：所有服務執行成功時，備份流程應完整完成
    /// </summary>
    [TestMethod]
    public async Task ExecuteAsync_WhenAllServicesSucceed_CompletesSuccessfully()
    {
        // Arrange - 建立實際的測試檔案
        Directory.CreateDirectory(_testTempDir);
        var backupFilePath = Path.Combine(_testTempDir, "test_20240101_120000.backup");
        await File.WriteAllTextAsync(backupFilePath, "test backup content");
        
        _mockDatabaseDumper
            .Setup(x => x.DumpAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(backupFilePath);

        _mockCloudStorageService
            .Setup(x => x.UploadFileAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _backupService.ExecuteAsync();

        // Assert
        _mockDatabaseDumper.Verify(
            x => x.DumpAsync(It.IsAny<CancellationToken>()),
            Times.Once);
        
        _mockCloudStorageService.Verify(
            x => x.UploadFileAsync(
                backupFilePath,
                "test_20240101_120000.backup",
                It.IsAny<CancellationToken>()),
            Times.Once);
        
        // 驗證備份檔案已被刪除
        Assert.IsFalse(File.Exists(backupFilePath));
    }

    /// <summary>
    /// 測試：資料庫匯出失敗時，應拋出例外
    /// </summary>
    [TestMethod]
    public async Task ExecuteAsync_WhenDumpFails_ThrowsException()
    {
        // Arrange
        _mockDatabaseDumper
            .Setup(x => x.DumpAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("pg_dump 執行失敗"));

        // Act & Assert
        await Assert.ThrowsExactlyAsync<InvalidOperationException>(
            async () => await _backupService.ExecuteAsync());
    }

    /// <summary>
    /// 測試：S3 上傳失敗時，應拋出例外
    /// </summary>
    [TestMethod]
    public async Task ExecuteAsync_WhenUploadFails_ThrowsException()
    {
        // Arrange - 建立實際的測試檔案
        Directory.CreateDirectory(_testTempDir);
        var backupFilePath = Path.Combine(_testTempDir, "test_20240101_120000.backup");
        await File.WriteAllTextAsync(backupFilePath, "test backup content");
        
        _mockDatabaseDumper
            .Setup(x => x.DumpAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(backupFilePath);

        _mockCloudStorageService
            .Setup(x => x.UploadFileAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("R2 上傳失敗"));

        // Act & Assert
        await Assert.ThrowsExactlyAsync<InvalidOperationException>(
            async () => await _backupService.ExecuteAsync());
        
        // 驗證上傳失敗時保留備份檔案
        Assert.IsTrue(File.Exists(backupFilePath));
    }

    /// <summary>
    /// 測試：當設定為保留本地檔案時，不應刪除備份檔案
    /// </summary>
    [TestMethod]
    public async Task ExecuteAsync_WhenRetainLocalFileIsTrue_DoesNotDeleteFile()
    {
        // Arrange - 建立實際的測試檔案
        Directory.CreateDirectory(_testTempDir);
        var backupFilePath = Path.Combine(_testTempDir, "test_20240101_120000.backup");
        await File.WriteAllTextAsync(backupFilePath, "test backup content");
        
        var retainSettings = Options.Create(new BackupSettings
        {
            TempDirectory = _testTempDir,
            FileNamePrefix = "test",
            RetainLocalFileAfterUpload = true // 保留本地檔案
        });

        var backupServiceWithRetain = new BackupService(
            _mockDatabaseDumper.Object,
            _mockCloudStorageService.Object,
            retainSettings,
            _mockLogger.Object);

        _mockDatabaseDumper
            .Setup(x => x.DumpAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(backupFilePath);

        _mockCloudStorageService
            .Setup(x => x.UploadFileAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await backupServiceWithRetain.ExecuteAsync();

        // Assert
        _mockCloudStorageService.Verify(
            x => x.UploadFileAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
        
        // 驗證備份檔案仍然存在
        Assert.IsTrue(File.Exists(backupFilePath));
    }
}
