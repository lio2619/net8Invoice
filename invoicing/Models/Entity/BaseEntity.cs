using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invoicing.Models.Entity
{
    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //public Guid UniqueId { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime? LastModified { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
