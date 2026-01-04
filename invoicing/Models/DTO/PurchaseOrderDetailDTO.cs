using System.ComponentModel;

namespace invoicing.Models.DTO
{
    /// <summary>
    /// 採購單明細 DTO（無單價、金額欄位）
    /// </summary>
    public class PurchaseOrderDetailDTO
    {
        [DisplayName("貨品編號")]
        public string? ProductCode { get; set; }

        [DisplayName("品名")]
        public string? ProductName { get; set; }

        [DisplayName("數量")]
        public string? Quantity { get; set; }

        [DisplayName("基本單位")]
        public string? Unit { get; set; }

        [DisplayName("備註")]
        public string? Remark { get; set; }
    }
}
