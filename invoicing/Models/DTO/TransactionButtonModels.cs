using invoicing.Models.DTO;

namespace invoicing.Models.DTO
{
    /// <summary>
    /// 儲存交易請求參數
    /// </summary>
    public class SaveTransactionRequest
    {
        /// <summary>
        /// 交易單子類型（如「出貨退出單」）
        /// </summary>
        public string OrderType { get; set; } = string.Empty;

        /// <summary>
        /// 客戶名稱
        /// </summary>
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// 日期（yyyyMMdd 格式）
        /// </summary>
        public string Date { get; set; } = string.Empty;

        /// <summary>
        /// 備註
        /// </summary>
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 總金額
        /// </summary>
        public string TotalAmount { get; set; } = "0";

        /// <summary>
        /// 單子編號（更新時使用，新增時為空）- 舉使用於內部關聯
        /// </summary>
        public string? OrderNumber { get; set; }

        /// <summary>
        /// 新單子編號（每月20號重置，4位數顯示）- 用於顯示和 Excel
        /// </summary>
        public string? NewOrderNumber { get; set; }

        /// <summary>
        /// 是否為更新模式
        /// </summary>
        public bool IsUpdate { get; set; } = false;

        /// <summary>
        /// 明細資料
        /// </summary>
        public List<InvoicingDetailDTO> Details { get; set; } = new();
    }

    /// <summary>
    /// 儲存結果
    /// </summary>
    public class SaveResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 訊息
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 儲存後的單子編號（內部關聯用）
        /// </summary>
        public string? OrderNumber { get; set; }

        /// <summary>
        /// 新單子編號（每月20號重置，4位數顯示）
        /// </summary>
        public string? NewOrderNumber { get; set; }
    }

    /// <summary>
    /// 刷新結果
    /// </summary>
    public class RefreshResult
    {
        /// <summary>
        /// 是否確認刷新
        /// </summary>
        public bool Confirmed { get; set; }
    }

    /// <summary>
    /// 刪除結果
    /// </summary>
    public class DeleteResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 訊息
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>
    /// 建立 Excel 請求參數
    /// </summary>
    public class CreateExcelRequest
    {
        /// <summary>
        /// 交易單子類型
        /// </summary>
        public string OrderType { get; set; } = string.Empty;

        /// <summary>
        /// 客戶名稱
        /// </summary>
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 總金額
        /// </summary>
        public string TotalAmount { get; set; } = "0";

        /// <summary>
        /// 明細資料
        /// </summary>
        public List<InvoicingDetailDTO> Details { get; set; } = new();

        /// <summary>
        /// 舊單子編號（舊資料使用）
        /// </summary>
        public string? OrderNumber { get; set; }

        /// <summary>
        /// 新單子編號（新資料使用，4位數顯示）
        /// </summary>
        public string? NewOrderNumber { get; set; }

        /// <summary>
        /// 是否使用新編號系統
        /// </summary>
        public bool IsUsingNewOrderNumber { get; set; }
    }
}
