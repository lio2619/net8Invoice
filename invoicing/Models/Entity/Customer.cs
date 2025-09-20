using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invoicing.Models.Entity
{
    /// <summary>
    /// 客戶
    /// </summary>
    [Table("Customer")]
    public class Customer : BaseEntity
    {
        [Comment("公司全名")]
        [Required]
        [MaxLength(100)]
        public string CompanyFullName { get; set; }

        [Comment("傳真號碼")]
        [MaxLength(30)]
        public string? FaxNumber { get; set; }

        [Comment("公司編號")]
        [MaxLength(10)]
        public string? CompanyCode { get; set; }

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

        [Comment("聯絡電話二")]
        [MaxLength(30)]
        public string? Phone2 { get; set; }

        [Comment("送貨地址")]
        [MaxLength(100)]
        public string? DeliveryAddress { get; set; }

        [Comment("送貨地郵遞區號")]
        [MaxLength(10)]
        public string? DeliveryZipCode { get; set; }
    }
}
