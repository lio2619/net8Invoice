using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invoicing.Models.Entity
{
    /// <summary>
    /// 整張儲存
    /// </summary>
    [Table("OrderDetail")]
    public class OrderDetail : BaseEntity
    {
        [Comment("備註")]
        [MaxLength(255)]
        public string? Remark { get; set; }

        [Comment("品名")]
        [MaxLength(60)]
        public string? ProductName { get; set; }

        [Comment("單價")]
        [MaxLength(50)]
        public string? UnitPrice { get; set; }

        [Required]
        [Comment("單子編號")]
        [MaxLength(50)]
        public string OrderNumber { get; set; }

        [Comment("基本單位")]
        [MaxLength(10)]
        public string? Unit { get; set; }

        [Comment("數量")]
        [MaxLength(10)]
        public string? Quantity { get; set; }

        [Comment("貨品編號")]
        [MaxLength(20)]
        public string? ProductCode { get; set; }

        [Comment("金額")]
        [MaxLength(50)]
        public string? Amount { get; set; }
    }
}
