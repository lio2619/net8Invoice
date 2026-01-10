using invoicing.Models.DTO;
using System.Data;

namespace invoicing.Service.Interface
{
    /// <summary>
    /// PDF 匯入服務介面（關貿專用）
    /// </summary>
    public interface IPdfImportService
    {
        /// <summary>
        /// 從 PDF 檔案匯入資料
        /// </summary>
        /// <param name="filePath">檔案路徑</param>
        /// <returns>匯入的訂單資料清單</returns>
        Task<List<PdfImportResult>> ImportFromPdfAsync(string filePath);
    }

    /// <summary>
    /// PDF 匯入結果
    /// </summary>
    public class PdfImportResult
    {
        /// <summary>
        /// 客戶名稱
        /// </summary>
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// 關貿採購單號
        /// </summary>
        public string PoNumber { get; set; } = string.Empty;

        /// <summary>
        /// 進銷存單子編號（新編號系統）
        /// </summary>
        public string NewOrderNumber { get; set; } = string.Empty;
    }
}
