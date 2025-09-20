using invoicing.DB.DBContext;
using invoicing.Models.Entity;
using invoicing.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace invoicing.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        protected readonly InvoicIngDbContext _context;
        public CustomerRepository(InvoicIngDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// 取的客戶資料裡面最大的客戶編號
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetMaxCompanyCode()
        {
            return await _context.Customers.Where(x => !x.IsDeleted).Select(y => Convert.ToInt32(y.CompanyCode))
                                            .DefaultIfEmpty(0).MaxAsync();
        }
    }
}
