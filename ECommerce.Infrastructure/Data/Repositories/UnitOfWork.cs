using ECommerce.Application.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data.Repositories
{
    public class UnitOfWork(ECommerceDbContext dbContext) : IUnitOfWork
    {
        private bool disposed = false;

        private readonly ECommerceDbContext _dbContext = dbContext;

        private readonly Dictionary<Type, object> _repositories = [];
        public IBaseRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return (IBaseRepository<T>)_repositories[typeof(T)] as IBaseRepository<T>;
            }

            var repositoryInstance = new BaseRepository<T>(_dbContext);
            _repositories.Add(typeof(T), repositoryInstance);
            return repositoryInstance;
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        // Implement raw SQL query method
        public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sql, params object[] parameters) where T : class
        {
            return await _dbContext.Set<T>().FromSqlRaw(sql, parameters).ToListAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void BeginTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
        }

        public void RollBack()
        {
            _dbContext.Database.RollbackTransaction();
        }
    }
}
