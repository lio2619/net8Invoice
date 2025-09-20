using invoicing.DB.DBContext;
using invoicing.Models.Entity;
using invoicing.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace invoicing.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly InvoicIngDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(InvoicIngDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task AddAsync(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            entity.LastModified = DateTime.UtcNow;
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetById(id).FirstOrDefaultAsync();
            if (entity != null)
            {
                entity.IsDeleted = true;
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
            }
        }

        public virtual IQueryable<T> Get()
        {
            return _dbSet.Where(e => !e.IsDeleted).AsNoTracking();
        }

        public virtual IQueryable<T> GetById(int id)
        {
            return _dbSet.Where(e => e.Id == id && !e.IsDeleted).AsNoTracking();
        }

        /// <summary>
        /// 根據條件查詢資料
        /// </summary>
        /// <param name="predicate">Lambda 運算式 (Where 條件)</param>
        /// <returns></returns>
        public virtual IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(e => !e.IsDeleted).Where(predicate).AsNoTracking();
        }

        public virtual IQueryable<T> GetWithInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.Where(e => !e.IsDeleted).Where(predicate).AsNoTracking();
        }
    }
}
