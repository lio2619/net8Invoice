using invoicing.Models.Entity;

namespace invoicing.Repository.Interface
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        /// <summary>
        /// 根據產品編號前綴查詢符合的產品編號清單
        /// </summary>
        /// <param name="prefix">產品編號前綴</param>
        /// <returns>符合條件的產品編號清單</returns>
        Task<List<string>> GetProductCodesByPrefixAsync(string prefix);

        /// <summary>
        /// 根據產品編號前綴查詢符合的產品編號與名稱
        /// </summary>
        /// <param name="prefix">產品編號前綴</param>
        /// <returns>符合條件的產品編號與名稱 Tuple 清單</returns>
        Task<List<(string ProductCode, string ProductName)>> GetProductCodesWithNameByPrefixAsync(string prefix);
    }
}
