using invoicing.Models.DTO;
using System.Data;

namespace invoicing.Service.Interface
{
    /// <summary>
    /// Excel 匯入服務介面
    /// </summary>
    public interface IExcelImportService
    {
        /// <summary>
        /// 從 Excel 檔案匯入資料
        /// </summary>
        /// <param name="filePath">檔案路徑</param>
        /// <param name="config">匯入設定</param>
        /// <param name="pluginFormService">外掛表單服務（用於查詢貨品資料）</param>
        /// <returns>匯入的訂單資料</returns>
        Task<ImportedOrderData> ImportFromExcelAsync(string filePath, ExcelImportConfig config, IPluginFormService pluginFormService);
    }
}
