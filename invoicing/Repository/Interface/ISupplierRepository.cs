using invoicing.Models.Entity;
using invoicing.Service.Interface;

namespace invoicing.Repository.Interface
{
    public interface ISupplierRepository : IBaseRepository<Supplier>
    {
        /// <summary>
        /// 取的廠商資料裡面最大的廠商編號
        /// </summary>
        /// <returns></returns>
        Task<int> GetMaxSupplierCode();
    }
}
