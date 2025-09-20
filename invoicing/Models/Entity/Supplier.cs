using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invoicing.Models.Entity
{
    /// <summary>
    /// 廠商
    /// </summary>
    [Table("Supplier")]
    public class Supplier : BaseEntity
    {
        [Comment("公司全名")]
        [Required]
        [MaxLength(100)]
        public string CompanyFullName { get; set; }

        [Comment("備註")]
        [MaxLength(100)]
        public string? Remark { get; set; }

        [Comment("傳真號碼")]
        [MaxLength(30)]
        public string? FaxNumber { get; set; }

        [Comment("公司簡稱")]
        [MaxLength(50)]
        public string? CompanyShortName { get; set; }

        [Comment("公司編號")]
        [MaxLength(10)]
        public string? CompanyCode { get; set; }

        [Comment("帳單地址")]
        [MaxLength(100)]
        public string? BillingAddress { get; set; }

        [Comment("登記地址")]
        [MaxLength(100)]
        public string? RegisteredAddress { get; set; }

        [Comment("發票抬頭")]
        [MaxLength(100)]
        public string? InvoiceTitle { get; set; }

        [Comment("統一編號")]
        [MaxLength(30)]
        public string? TaxId { get; set; }

        [Comment("聯絡人一")]
        [MaxLength(30)]
        public string? ContactPerson1 { get; set; }

        [Comment("聯絡電話一")]
        [MaxLength(30)]
        public string? Phone1 { get; set; }

        [Comment("負責人")]
        [MaxLength(30)]
        public string? ResponsiblePerson { get; set; }

        [Comment("送貨地址")]
        [MaxLength(100)]
        public string? DeliveryAddress { get; set; }
    }
}
