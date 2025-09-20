using invoicing.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace invoicing.DB.DBContext
{
    public class InvoicIngDbContext : DbContext
    {
        public InvoicIngDbContext(DbContextOptions<InvoicIngDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 由於我們已在 Entity 中使用 [ForeignKey] 等屬性定義了關聯，
            // EF Core 的慣例會自動處理，此處不需要額外的 Fluent API 設定。
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SuggestedPrice> SuggestedPrices { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
