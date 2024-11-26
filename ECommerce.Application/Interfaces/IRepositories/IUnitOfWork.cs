namespace ECommerce.Application.Interfaces.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        public IBaseRepository<T> GetRepository<T>() where T : class;
        Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sql, params object[] parameters) where T : class;
        Task SaveAsync();
        void BeginTransaction();
        void CommitTransaction();
        void RollBack();
    }
}
