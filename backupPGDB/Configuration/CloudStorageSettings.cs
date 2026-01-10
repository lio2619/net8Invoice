namespace backupPGDB.Configuration;

/// <summary>
/// Cloudflare R2 雲端儲存設定 (Strongly Typed Configuration)
/// </summary>
public sealed class CloudStorageSettings
{
    /// <summary>
    /// R2 Endpoint URL (例如: https://xxx.r2.cloudflarestorage.com)
    /// </summary>
    public string Endpoint { get; set; } = string.Empty;

    /// <summary>
    /// R2 Access Key ID (來自 appsettings.Secret.json)
    /// </summary>
    public string AccessKeyId { get; set; } = string.Empty;

    /// <summary>
    /// R2 Secret Access Key (來自 appsettings.Secret.json)
    /// </summary>
    public string SecretAccessKey { get; set; } = string.Empty;

    /// <summary>
    /// R2 Bucket 名稱
    /// </summary>
    public string BucketName { get; set; } = string.Empty;

    /// <summary>
    /// 區域設定 (R2 使用 "auto")
    /// </summary>
    public string Region { get; set; } = "auto";

    /// <summary>
    /// 是否強制使用路徑樣式 (R2 必須為 true)
    /// </summary>
    public bool ForcePathStyle { get; set; } = true;
}
