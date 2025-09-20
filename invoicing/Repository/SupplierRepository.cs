using invoicing.DB.DBContext;
using invoicing.Models.Entity;
using invoicing.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace invoicing.Repository
{
    public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
    {
        protected readonly InvoicIngDbContext _context;
        public SupplierRepository(InvoicIngDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// 取的廠商資料裡面最大的廠商編號
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetMaxSupplierCode()
        {
            return await _context.Suppliers.Where(x => !x.IsDeleted).Select(y => Convert.ToInt32(y.CompanyCode))
                                            .DefaultIfEmpty(0).MaxAsync();
        }
    }
}
