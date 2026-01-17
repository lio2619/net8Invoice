namespace invoicing.Models.DTO
{
    /// <summary>
    /// 應收帳款列印請求 DTO
    /// </summary>
    public class AccountsReceivablePrintRequest
    {
        /// <summary>
        /// 客戶名稱
        /// </summary>
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// 帳款區間起始日期 (格式：yyyyMMdd)
        /// </summary>
        public string StartDate { get; set; } = string.Empty;

        /// <summary>
        /// 帳款區間結束日期 (格式：yyyyMMdd)
        /// </summary>
        public string EndDate { get; set; } = string.Empty;

        /// <summary>
        /// 本期合計
        /// </summary>
        public decimal SubTotal { get; set; }

        /// <summary>
        /// 營業稅
        /// </summary>
        public decimal Tax { get; set; }

        /// <summary>
        /// 本期總計
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// 明細清單
        /// </summary>
        public List<AccountsReceivablePrintDetail> Details { get; set; } = new();
    }

    /// <summary>
    /// 應收帳款列印明細 DTO
    /// </summary>
    public class AccountsReceivablePrintDetail
    {
        /// <summary>
        /// 單別 (出貨單/出貨退出單)
        /// </summary>
        public string OrderType { get; set; } = string.Empty;

        /// <summary>
        /// 交易日期
        /// </summary>
        public string TransactionDate { get; set; } = string.Empty;

        /// <summary>
        /// 交易單號
        /// </summary>
        public string TransactionNumber { get; set; } = string.Empty;

        /// <summary>
        /// 合計金額
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 統計金額 (出貨退出單為負數)
        /// </summary>
        public decimal StatisticalAmount { get; set; }
    }
}
