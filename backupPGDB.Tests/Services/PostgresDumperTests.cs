using backupPGDB.Configuration;
using backupPGDB.Interfaces;
using backupPGDB.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace backupPGDB.Tests.Services;

/// <summary>
/// PostgresDumper 單元測試
/// </summary>
[TestClass]
public class PostgresDumperTests
{
    private Mock<IProcessRunner> _mockProcessRunner = null!;
    private Mock<ILogger<PostgresDumper>> _mockLogger = null!;
    private IOptions<DatabaseSettings> _databaseSettings = null!;
    private IOptions<BackupSettings> _backupSettings = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockProcessRunner = new Mock<IProcessRunner>();
        _mockLogger = new Mock<ILogger<PostgresDumper>>();

        _databaseSettings = Options.Create(new DatabaseSettings
        {
            Host = "localhost",
            Port = 5432,
            DatabaseName = "testdb",
            Username = "testuser",
            Password = "testpassword",
            PgDumpPath = @"C:\Program Files\PostgreSQL\17\bin\pg_dump.exe"
        });

        _backupSettings = Options.Create(new BackupSettings
        {
            TempDirectory = @"C:\Temp\Test",
            FileNamePrefix = "test",
            RetainLocalFileAfterUpload = false
        });
    }

    /// <summary>
    /// 測試：pg_dump 指令應包含正確的參數
    /// </summary>
    [TestMethod]
    public async Task DumpAsync_BuildsCorrectPgDumpCommand()
    {
        // Arrange
        _mockProcessRunner
            .Setup(x => x.RunAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<IDictionary<string, string>?>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProcessResult(0, "", ""));

        var dumper = new PostgresDumper(
            _mockProcessRunner.Object,
            _databaseSettings,
            _backupSettings,
            _mockLogger.Object);

        // Act
        await dumper.DumpAsync();

        // Assert
        _mockProcessRunner.Verify(
            x => x.RunAsync(
                @"C:\Program Files\PostgreSQL\17\bin\pg_dump.exe",
                It.Is<string>(args =>
                    args.Contains("-h localhost") &&
                    args.Contains("-p 5432") &&
                    args.Contains("-U testuser") &&
                    args.Contains("-d testdb") &&
                    args.Contains("-Fc")),
                It.Is<IDictionary<string, string>>(env =>
                    env != null && env["PGPASSWORD"] == "testpassword"),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    /// <summary>
    /// 測試：當 pg_dump 執行失敗時，應拋出例外
    /// </summary>
    [TestMethod]
    public async Task DumpAsync_WhenProcessFails_ThrowsException()
    {
        // Arrange
        _mockProcessRunner
            .Setup(x => x.RunAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<IDictionary<string, string>?>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProcessResult(1, "", "pg_dump: error: connection failed"));

        var dumper = new PostgresDumper(
            _mockProcessRunner.Object,
            _databaseSettings,
            _backupSettings,
            _mockLogger.Object);

        // Act & Assert
        await Assert.ThrowsExactlyAsync<InvalidOperationException>(
            async () => await dumper.DumpAsync());
    }

    /// <summary>
    /// 測試：成功執行時，應回傳備份檔案路徑
    /// </summary>
    [TestMethod]
    public async Task DumpAsync_WhenSuccessful_ReturnsBackupFilePath()
    {
        // Arrange
        _mockProcessRunner
            .Setup(x => x.RunAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<IDictionary<string, string>?>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProcessResult(0, "", ""));

        var dumper = new PostgresDumper(
            _mockProcessRunner.Object,
            _databaseSettings,
            _backupSettings,
            _mockLogger.Object);

        // Act
        var result = await dumper.DumpAsync();

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.StartsWith(_backupSettings.Value.TempDirectory));
        Assert.IsTrue(result.Contains("test_")); // FileNamePrefix
        Assert.IsTrue(result.EndsWith(".backup"));
    }

    /// <summary>
    /// 測試：密碼應透過環境變數 PGPASSWORD 傳遞
    /// </summary>
    [TestMethod]
    public async Task DumpAsync_PassesPasswordViaEnvironmentVariable()
    {
        // Arrange
        IDictionary<string, string>? capturedEnv = null;

        _mockProcessRunner
            .Setup(x => x.RunAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<IDictionary<string, string>?>(),
                It.IsAny<CancellationToken>()))
            .Callback<string, string, IDictionary<string, string>?, CancellationToken>(
                (_, _, env, _) => capturedEnv = env)
            .ReturnsAsync(new ProcessResult(0, "", ""));

        var dumper = new PostgresDumper(
            _mockProcessRunner.Object,
            _databaseSettings,
            _backupSettings,
            _mockLogger.Object);

        // Act
        await dumper.DumpAsync();

        // Assert
        Assert.IsNotNull(capturedEnv);
        Assert.IsTrue(capturedEnv.ContainsKey("PGPASSWORD"));
        Assert.AreEqual("testpassword", capturedEnv["PGPASSWORD"]);
    }
}
