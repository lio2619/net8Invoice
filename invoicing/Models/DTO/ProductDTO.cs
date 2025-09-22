using System.ComponentModel;

namespace invoicing.Models.DTO
{
    public class ProductDTO
    {
        [DisplayName("貨品編號")]
        public string ProductCode { get; set; }

        [DisplayName("品名")]
        public string? ProductName { get; set; }

        [DisplayName("標準售價")]
        public decimal? StandardPrice { get; set; }

        [DisplayName("售價A")]
        public decimal? PriceA { get; set; }

        [DisplayName("售價B")]
        public decimal? PriceB { get; set; }

        [DisplayName("標準成本")]
        public decimal? StandardCost { get; set; }
    }
}
