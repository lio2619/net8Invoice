using invoicing.Models.DTO;

namespace invoicing.Service.Interface
{
    /// <summary>
    /// 財務報表服務介面
    /// </summary>
    public interface IFinancialService
    {
        /// <summary>
        /// 依日期區間和單據類型查詢，並依客戶、單子分組彙總
        /// </summary>
        /// <param name="startDate">起始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="orderTypes">單據類型陣列</param>
        /// <returns>分組彙總結果</returns>
        Task<List<FinancialSummaryDto>> GetGroupedTotalByDateRangeAsync(
            DateTime startDate, DateTime endDate, string[] orderTypes);

        /// <summary>
        /// 計算淨總額（正向單據 - 反向單據）
        /// </summary>
        /// <param name="startDate">起始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="positiveOrderType">正向單據類型（如：出貨單、進貨單）</param>
        /// <param name="negativeOrderType">負向單據類型（如：出貨退出單、進貨退出單）</param>
        /// <returns>淨總額</returns>
        Task<decimal> CalculateNetTotalAsync(
            DateTime startDate, DateTime endDate,
            string positiveOrderType, string negativeOrderType);

        /// <summary>
        /// 依日期區間查詢有資料的客戶清單（去重）
        /// </summary>
        /// <param name="startDate">起始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="orderTypes">單據類型陣列</param>
        /// <returns>客戶名稱清單</returns>
        Task<List<string>> GetDistinctCustomersAsync(
            DateTime startDate, DateTime endDate, string[] orderTypes);

        /// <summary>
        /// 依日期區間和客戶查詢單據明細
        /// </summary>
        /// <param name="startDate">起始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="customer">客戶名稱</param>
        /// <param name="orderTypes">單據類型陣列</param>
        /// <returns>應收帳款明細</returns>
        Task<List<AccountsReceivableDto>> GetOrderDetailsByCustomerAsync(
            DateTime startDate, DateTime endDate,
            string customer, string[] orderTypes);
    }
}
