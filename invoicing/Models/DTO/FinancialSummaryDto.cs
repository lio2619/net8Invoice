namespace invoicing.Models.DTO
{
    /// <summary>
    /// 財務彙總 DTO（用於 TotalSalesForm / TotalPurchasesForm）
    /// </summary>
    public class FinancialSummaryDto
    {
        /// <summary>
        /// 客戶名稱
        /// </summary>
        public string Customer { get; set; } = string.Empty;

        /// <summary>
        /// 單子類型
        /// </summary>
        public string OrderName { get; set; } = string.Empty;

        /// <summary>
        /// 金額
        /// </summary>
        public decimal Amount { get; set; }
    }
}
