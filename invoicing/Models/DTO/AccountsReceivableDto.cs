namespace invoicing.Models.DTO
{
    /// <summary>
    /// 應收帳款明細 DTO
    /// </summary>
    public class AccountsReceivableDto
    {
        /// <summary>
        /// 單子類型（出貨單/出貨退出單）
        /// </summary>
        public string OrderName { get; set; } = string.Empty;

        /// <summary>
        /// 日期（格式：yyyyMMdd）
        /// </summary>
        public string Date { get; set; } = string.Empty;

        /// <summary>
        /// 單據唯一識別碼（日期+編號）
        /// </summary>
        public string OrderUid { get; set; } = string.Empty;

        /// <summary>
        /// 總金額
        /// </summary>
        public decimal TotalAmount { get; set; }
    }
}
