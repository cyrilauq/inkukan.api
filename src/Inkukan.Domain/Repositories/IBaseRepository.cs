namespace Inkukan.Domain.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        IQueryable<T> GetQuery();
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    }
}
