using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> Entities { get; }
        Task<T?> GetByIdAsync(string id, bool asNoTracking = false);
        Task<IEnumerable<T>> GetPaginationListAsync(
            IQueryable<T> query,
            int index,
            int pageSize,
            bool asNoTracking = false);
        Task InsertAsync(T obj);
        Task InsertRangeAsync(IEnumerable<T> objs);
        Task UpdateAsync(T obj);
        Task UpdateRangeAsync(IEnumerable<T> objs);
        Task DeleteAsync(object id);
    }
}
