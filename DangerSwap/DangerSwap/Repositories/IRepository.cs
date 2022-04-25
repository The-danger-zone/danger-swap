namespace DangerSwap.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetEntitiesAsync();
        Task<T> GetEntity(Guid id);
        Task UpdateEntity(T entity);
        Task CreateEntity(T entity);
        Task DeleteEntity(Guid id);
        Task<bool> IsExist(Guid id);
    }
}
