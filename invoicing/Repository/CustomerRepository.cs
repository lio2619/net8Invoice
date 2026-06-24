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
            // 先把需要的欄位撈回記憶體 (Client-side)
            var companyCodes = await _context.Customers
                .Where(x => !x.IsDeleted)
                .Select(x => x.CompanyCode)
                .ToListAsync(); // 這一步會真正執行 SQL 查詢

            // 接下來在記憶體中做轉型與轉換，就不會觸發 LINQ 翻譯錯誤了
            return companyCodes
                .Select(y => int.TryParse(y, out int num) ? num : 0)
                .DefaultIfEmpty(0)
                .Max();
        }
    }
}
