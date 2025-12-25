using invoicing.DB.DBContext;
using invoicing.Models.Entity;
using invoicing.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace invoicing.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        protected readonly InvoicIngDbContext _context;
        public ProductRepository(InvoicIngDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// 根據產品編號前綴查詢符合的產品編號清單
        /// </summary>
        /// <param name="prefix">產品編號前綴</param>
        /// <returns>符合條件的產品編號清單（最多 10 筆）</returns>
        public async Task<List<string>> GetProductCodesByPrefixAsync(string prefix)
        {
            if (string.IsNullOrWhiteSpace(prefix))
                return new List<string>();

            return await _context.Set<Product>()
                .Where(p => !p.IsDeleted)
                .Where(p => p.ProductCode.StartsWith(prefix))
                .Select(p => p.ProductCode)
                .Take(10)
                .ToListAsync();
        }

        /// <summary>
        /// 根據產品編號前綴查詢符合的產品編號與名稱
        /// </summary>
        /// <param name="prefix">產品編號前綴</param>
        /// <returns>符合條件的產品編號與名稱 Tuple 清單（最多 10 筆）</returns>
        public async Task<List<(string ProductCode, string ProductName)>> GetProductCodesWithNameByPrefixAsync(string prefix)
        {
            if (string.IsNullOrWhiteSpace(prefix))
                return new List<(string, string)>();

            var results = await _context.Set<Product>()
                .Where(p => !p.IsDeleted)
                .Where(p => p.ProductCode.StartsWith(prefix))
                .Select(p => new { p.ProductCode, p.ProductName })
                .Take(10)
                .ToListAsync();

            return results.Select(r => (r.ProductCode, r.ProductName ?? "")).ToList();
        }
    }
}
