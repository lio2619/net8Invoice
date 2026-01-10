namespace backupPGDB.Interfaces;

/// <summary>
/// 資料庫匯出介面
/// 負責產生資料庫備份檔案
/// </summary>
public interface IDatabaseDumper
{
    /// <summary>
    /// 非同步執行資料庫匯出
    /// </summary>
    /// <param name="cancellationToken">取消權杖</param>
    /// <returns>備份檔案的完整路徑</returns>
    Task<string> DumpAsync(CancellationToken cancellationToken = default);
}
