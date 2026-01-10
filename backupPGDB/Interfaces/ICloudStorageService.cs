namespace backupPGDB.Interfaces;

/// <summary>
/// 雲端儲存服務介面
/// 封裝雲端儲存操作 (相容 AWS S3 API)
/// </summary>
public interface ICloudStorageService
{
    /// <summary>
    /// 非同步上傳檔案至雲端儲存
    /// </summary>
    /// <param name="localFilePath">本地檔案完整路徑</param>
    /// <param name="remoteKey">遠端儲存的物件鍵值 (檔案名稱)</param>
    /// <param name="cancellationToken">取消權杖</param>
    Task UploadFileAsync(
        string localFilePath,
        string remoteKey,
        CancellationToken cancellationToken = default);
}
