using System.ComponentModel;

namespace invoicing.Models.DTO
{
    public class CustomerDto
    {
        [DisplayName("公司全名")]
        public string? CompanyFullName { get; set; }

        [DisplayName("電話")]
        public string? Phone1 { get; set; }

        [DisplayName("傳真")]
        public string? FaxNumber { get; set; }

        [DisplayName("送貨地址")]
        public string? DeliveryAddress { get; set; }
    }

}
