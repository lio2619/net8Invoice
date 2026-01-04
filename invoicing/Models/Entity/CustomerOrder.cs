using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invoicing.Models.Entity
{
    /// <summary>
    /// 總單子_客戶
    /// </summary>
    [Table("CustomerOrder")]
    public class CustomerOrder : BaseEntity
    {
        [Comment("單子編號")]
        [MaxLength(50)]
        public string? OrderNumber { get; set; }

        [Comment("新單子編號")]
        [MaxLength(50)]
        public string? NewOrderNumber { get; set; }

        [Comment("備註")]
        [MaxLength(255)]
        public string? Remark { get; set; }

        [Comment("刪除")]
        [MaxLength(3)]
        public string? Deleted { get; set; }

        [Comment("單子")]
        [MaxLength(10)]
        public string? OrderName { get; set; }

        [Comment("客戶")]
        [MaxLength(30)]
        public string? Customer { get; set; }

        [Comment("日期")]
        [MaxLength(8)]
        public string? Date { get; set; }

        [Comment("時間")]
        [MaxLength(14)]
        public string? Time { get; set; }

        [Comment("總金額")]
        [MaxLength(50)]
        public string? TotalAmount { get; set; }

        [Comment("關貿")]
        [MaxLength(3)]
        public string? Customs { get; set; }
    }
}
