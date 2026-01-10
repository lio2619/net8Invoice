using System.Data;

namespace invoicing.Service.Interface
{
    /// <summary>
    /// 外掛表單共用服務介面
    /// </summary>
    public interface IPluginFormService
    {
        /// <summary>
        /// 建立來源資料表結構
        /// </summary>
        DataTable CreateSourceDataTable();

        /// <summary>
        /// 載入客戶清單
        /// </summary>
        Task<List<string>> LoadCustomersAsync();

        /// <summary>
        /// 儲存訂單至資料庫
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="customerName">客戶名稱</param>
        /// <param name="remark">備註</param>
        /// <param name="totalAmount">總金額</param>
        /// <param name="sourceTable">來源資料表</param>
        /// <returns>訂單編號</returns>
        Task<string> SaveOrderAsync(DateTime date, string customerName, string remark, string totalAmount, DataTable sourceTable);

        /// <summary>
        /// 取得客戶資訊
        /// </summary>
        /// <param name="customerName">客戶名稱</param>
        Task<CustomerInfo?> GetCustomerInfoAsync(string customerName);

        /// <summary>
        /// 取得訂單編號
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="customerName">客戶名稱</param>
        /// <param name="remark">備註</param>
        /// <param name="totalAmount">總金額</param>
        Task<int?> GetOrderNumberAsync(DateTime date, string customerName, string remark, string totalAmount);

        /// <summary>
        /// 新增貨品主檔
        /// </summary>
        Task InsertProductAsync(string productCode, string productName, string unit);

        /// <summary>
        /// 取得貨品資訊
        /// </summary>
        Task<ProductInfo?> GetProductInfoAsync(string productCode);

        /// <summary>
        /// 取得建議售價
        /// </summary>
        Task<string?> GetSuggestedPriceAsync(string standardPrice);
    }

    /// <summary>
    /// 客戶資訊
    /// </summary>
    public class CustomerInfo
    {
        public string Phone { get; set; } = string.Empty;
        public string Fax { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }

    /// <summary>
    /// 貨品資訊
    /// </summary>
    public class ProductInfo
    {
        public string ProductCode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public string StandardPrice { get; set; } = string.Empty;
        public string StandardCost { get; set; } = string.Empty;
    }
}
