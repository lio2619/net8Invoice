namespace backupPGDB.Interfaces;

/// <summary>
/// 備份服務介面
/// 協調整個備份流程：資料庫匯出 -> 雲端上傳 -> 清理暫存
/// </summary>
public interface IBackupService
{
    /// <summary>
    /// 非同步執行完整備份流程
    /// </summary>
    /// <param name="cancellationToken">取消權杖</param>
    Task ExecuteAsync(CancellationToken cancellationToken = default);
}
