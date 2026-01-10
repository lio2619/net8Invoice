using System.Data;

namespace invoicing.Models.DTO
{
    /// <summary>
    /// 匯入訂單資料
    /// </summary>
    public class ImportedOrderData
    {
        /// <summary>
        /// 資料表（用於 DataGridView 綁定）
        /// </summary>
        public DataTable DataTable { get; set; } = new DataTable();

        /// <summary>
        /// 來源資料表（用於儲存至資料庫）
        /// </summary>
        public DataTable SourceTable { get; set; } = new DataTable();

        /// <summary>
        /// 總金額
        /// </summary>
        public double TotalAmount { get; set; }

        /// <summary>
        /// 明細列表
        /// </summary>
        public List<ImportedOrderDetail> Details { get; set; } = new();
    }

    /// <summary>
    /// 匯入訂單明細
    /// </summary>
    public class ImportedOrderDetail
    {
        public string ProductCode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string Quantity { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public string UnitPrice { get; set; } = string.Empty;
        public string Amount { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
    }
}
