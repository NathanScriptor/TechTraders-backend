using ECommerce.Application.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ECommerceDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;
        public BaseRepository(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public IQueryable<T> Entities => _dbContext.Set<T>();
        public async Task<T?> GetByIdAsync(string id, bool asNoTracking = false)
        {
            var query = asNoTracking ? _dbSet.AsNoTracking() : _dbSet;
            return await query.FirstOrDefaultAsync(e => EF.Property<string>(e, "Id") == id);
        }
        public async Task<IEnumerable<T>> GetPaginationListAsync(
            IQueryable<T> query,
            int index,
            int pageSize,
            bool asNoTracking = false)
        {
            var queryable = asNoTracking ? query.AsNoTracking() : query;
            return await queryable.Skip(index * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task InsertAsync(T obj) => await _dbSet.AddAsync(obj);
        public async Task InsertRangeAsync(IEnumerable<T> objs) => await _dbSet.AddRangeAsync(objs);
        public async Task UpdateAsync(T obj)
        {
            _dbSet.Update(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateRangeAsync(IEnumerable<T> objs)
        {
            _dbSet.UpdateRange(objs);
            foreach (var obj in objs)
            {
                _dbContext.Entry(obj).State = EntityState.Modified;
            }
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(object id)
        {
            T? obj = await _dbSet.FindAsync(id);
            if (obj != null)
            {
                _dbSet.Remove(obj);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
