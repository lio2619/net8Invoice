using System.Linq.Expressions;

namespace invoicing.Repository.Interface
{
    public interface IBaseRepository<T> where T : class
    {
        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);

        IQueryable<T> Get();

        IQueryable<T> GetById(int id);

        IQueryable<T> Get(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetWithInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    }
}
