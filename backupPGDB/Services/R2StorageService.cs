using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using backupPGDB.Configuration;
using backupPGDB.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace backupPGDB.Services;

/// <summary>
/// Cloudflare R2 雲端儲存服務
/// 使用 AWS SDK (S3 相容 API)
/// </summary>
public sealed class R2StorageService : ICloudStorageService, IDisposable
{
    private readonly IAmazonS3 _s3Client;
    private readonly CloudStorageSettings _settings;
    private readonly ILogger<R2StorageService> _logger;
    private bool _disposed;

    public R2StorageService(
        IOptions<CloudStorageSettings> settings,
        ILogger<R2StorageService> logger)
        : this(CreateS3Client(settings.Value), settings.Value, logger)
    {
    }

    /// <summary>
    /// 用於單元測試的建構函式，允許注入 Mock 的 IAmazonS3
    /// </summary>
    internal R2StorageService(
        IAmazonS3 s3Client,
        CloudStorageSettings settings,
        ILogger<R2StorageService> logger)
    {
        _s3Client = s3Client;
        _settings = settings;
        _logger = logger;
    }

    private static IAmazonS3 CreateS3Client(CloudStorageSettings settings)
    {
        var config = new AmazonS3Config
        {
            ServiceURL = settings.Endpoint,
            AuthenticationRegion = settings.Region,
            ForcePathStyle = settings.ForcePathStyle // R2 必須啟用
        };

        var credentials = new BasicAWSCredentials(
            settings.AccessKeyId,
            settings.SecretAccessKey);

        return new AmazonS3Client(credentials, config);
    }

    /// <inheritdoc/>
    public async Task UploadFileAsync(
        string localFilePath,
        string remoteKey,
        CancellationToken cancellationToken = default)
    {
        if (!File.Exists(localFilePath))
        {
            throw new FileNotFoundException("備份檔案不存在", localFilePath);
        }

        _logger.LogInformation("開始上傳檔案至 R2...");
        _logger.LogDebug("本地檔案: {LocalFilePath}", localFilePath);
        _logger.LogDebug("遠端路徑: {BucketName}/{RemoteKey}", _settings.BucketName, remoteKey);

        try
        {
            var request = new PutObjectRequest
            {
                BucketName = _settings.BucketName,
                Key = remoteKey,
                FilePath = localFilePath,
                DisablePayloadSigning = true // R2 建議開啟，避免簽章錯誤
            };

            await _s3Client.PutObjectAsync(request, cancellationToken);

            _logger.LogInformation("檔案上傳成功: {RemoteKey}", remoteKey);
        }
        catch (AmazonS3Exception ex)
        {
            _logger.LogError(ex, "S3 上傳失敗: {ErrorCode} - {Message}", ex.ErrorCode, ex.Message);
            throw new InvalidOperationException($"R2 上傳失敗: {ex.Message}", ex);
        }
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _s3Client.Dispose();
            _disposed = true;
        }
    }
}
