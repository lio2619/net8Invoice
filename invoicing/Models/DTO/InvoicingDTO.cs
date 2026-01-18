using System.ComponentModel;

namespace invoicing.Models.DTO
{
    public class InvoicingDTO
    {
        /// <summary>
        /// CustomerOrder 的 Id（用於新資料查詢明細）
        /// </summary>
        [Browsable(false)]
        public int Id { get; set; }

        /// <summary>
        /// 舊單子編號（內部使用，不顯示）
        /// </summary>
        [Browsable(false)]
        public string? OrderNumber { get; set; }

        /// <summary>
        /// 新單子編號（內部使用，不顯示）
        /// </summary>
        [Browsable(false)]
        public string? NewOrderNumber { get; set; }

        /// <summary>
        /// 顯示用的單子編號（優先顯示 NewOrderNumber，沒有則顯示 OrderNumber）
        /// </summary>
        [DisplayName("編號")]
        public string DisplayOrderNumber => !string.IsNullOrEmpty(NewOrderNumber) ? NewOrderNumber : OrderNumber;

        [DisplayName("日期")]
        public string? Date { get; set; }

        [DisplayName("客戶")]
        public string? Customer { get; set; }

        [DisplayName("單子")]
        public string? OrderName { get; set; }

        [DisplayName("總金額")]
        public string? TotalAmount { get; set; }

        /// <summary>
        /// 關貿標記（"1" 表示關貿資料）
        /// </summary>
        [Browsable(false)]
        public string? Customs { get; set; }

        [DisplayName("備註")]
        public string? Remark { get; set; }
    }
}

