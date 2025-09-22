using invoicing.DB.DBContext;
using invoicing.Models.Entity;
using invoicing.Repository.Interface;

namespace invoicing.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        protected readonly InvoicIngDbContext _context;
        public ProductRepository(InvoicIngDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
