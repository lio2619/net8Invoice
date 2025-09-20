using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invoicing.Models.Entity
{
    /// <summary>
    /// 貨品主檔
    /// </summary>
    [Table("Product")]
    [Index(nameof(ProductCode), IsUnique = true)]
    public class Product : BaseEntity
    {
        [Required]
        [Comment("貨品編號")]
        [MaxLength(20)]
        public string ProductCode { get; set; }

        [Comment("品名")]
        [MaxLength(60)]
        public string? ProductName { get; set; }

        [Comment("售價A")]
        public decimal? PriceA { get; set; }

        [Comment("售價B")]
        public decimal? PriceB { get; set; }

        [Comment("售價C")]
        public decimal? PriceC { get; set; }

        [Comment("售價D")]
        public decimal? PriceD { get; set; }

        [Comment("售價E")]
        public decimal? PriceE { get; set; }

        [Comment("基本單位")]
        [MaxLength(10)]
        public string? Unit { get; set; }

        [Comment("標準售價")]
        public decimal? StandardPrice { get; set; }

        [Comment("標準成本")]
        public decimal? StandardCost { get; set; }

        [Comment("現行成本")]
        public decimal? CurrentCost { get; set; }
    }
}
