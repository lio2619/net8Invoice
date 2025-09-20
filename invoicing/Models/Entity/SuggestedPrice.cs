using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invoicing.Models.Entity
{
    /// <summary>
    /// 建議售價
    /// </summary>
    [Table("SuggestedPrice")]
    public class SuggestedPrice : BaseEntity
    {
        [Required]
        [Comment("標準售價")]
        [MaxLength(50)]
        public string? StandardPrice { get; set; }

        [Comment("建議售價")]
        [MaxLength(50)]
        public string? SuggestedSalePrice { get; set; }
    }
}
