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

            modelBuilder.Entity<CustomerOrder>(entity =>
            {
                // 1. 設定 Unique Index (過濾掉 NULL)
                // 這樣可以確保若 OrderNumber 有值，則不可重複
                entity.HasIndex(e => e.OrderNumber)
                      .IsUnique()
                      .HasFilter("\"OrderNumber\" IS NOT NULL");

                entity.HasIndex(e => e.NewOrderNumber)
                      .IsUnique()
                      .HasFilter("\"NewOrderNumber\" IS NOT NULL");

                // 2. 設定 PostgreSQL Check Constraint (互斥邏輯)
                // 確保 (A有值且B為空) OR (A為空且B有值)
                entity.ToTable(t => t.HasCheckConstraint("CK_OrderNumber_Exclusive",
                    "(\"OrderNumber\" IS NOT NULL AND \"NewOrderNumber\" IS NULL) OR " +
                    "(\"OrderNumber\" IS NULL AND \"NewOrderNumber\" IS NOT NULL)"));
            });

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
