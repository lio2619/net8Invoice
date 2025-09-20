using invoicing.Models.Entity;

namespace invoicing.Repository.Interface
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        /// <summary>
        /// 取的客戶資料裡面最大的客戶編號
        /// </summary>
        /// <returns></returns>
        Task<int> GetMaxCompanyCode();
    }
}
