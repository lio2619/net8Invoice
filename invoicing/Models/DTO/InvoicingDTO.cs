using System.ComponentModel;

namespace invoicing.Models.DTO
{
    public class InvoicingDTO
    {
        [DisplayName("貨品編號")]
        public string? ProductCode { get; set; }

        [DisplayName("品名")]
        public string? ProductName { get; set; }

        [DisplayName("數量")]
        public string? Quantity { get; set; }

        [DisplayName("基本單位")]
        public string? Unit { get; set; }

        [DisplayName("單價")]
        public string? UnitPrice { get; set; }

        [DisplayName("金額")]
        public string? Amount { get; set; }

        [DisplayName("備註")]
        public string? Remark { get; set; }
    }
}
