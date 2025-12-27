using System.ComponentModel;

namespace invoicing.Models.DTO
{
    public class InvoicingDTO
    {
        [DisplayName("編號")]
        public string OrderNumber { get; set; }

        [DisplayName("日期")]
        public string? Date { get; set; }

        [DisplayName("客戶")]
        public string? Customer { get; set; }

        [DisplayName("單子")]
        public string? OrderName { get; set; }

        [DisplayName("總金額")]
        public string? TotalAmount { get; set; }

        [DisplayName("備註")]
        public string? Remark { get; set; }
    }
}
