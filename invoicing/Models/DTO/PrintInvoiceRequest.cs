namespace invoicing.Models.DTO
{
    /// <summary>
    /// 列印發票請求 DTO
    /// </summary>
    public class PrintInvoiceRequest
    {
        /// <summary>
        /// 單子類型（出貨單、訂貨單、採購單等）
        /// </summary>
        public string OrderType { get; set; } = string.Empty;

        /// <summary>
        /// 客戶或廠商名稱
        /// </summary>
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// 電話
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// 傳真
        /// </summary>
        public string Fax { get; set; } = string.Empty;

        /// <summary>
        /// 送貨地址
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// 日期（格式：yyyyMMdd 或顯示用格式）
        /// </summary>
        public string Date { get; set; } = string.Empty;

        /// <summary>
        /// 單子編號
        /// </summary>
        public string OrderNumber { get; set; } = string.Empty;

        /// <summary>
        /// 備註
        /// </summary>
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 總金額
        /// </summary>
        public string TotalAmount { get; set; } = string.Empty;

        /// <summary>
        /// 是否為廠商單據（決定表頭顯示「廠商名稱」或「客戶名稱」）
        /// </summary>
        public bool IsSupplier { get; set; }

        /// <summary>
        /// 發票明細清單（適用於有單價金額的表單）
        /// </summary>
        public List<InvoicingDetailDTO>? Details { get; set; }

        /// <summary>
        /// 採購單明細清單（無單價金額）
        /// </summary>
        public List<PurchaseOrderDetailDTO>? PurchaseOrderDetails { get; set; }
    }
}
