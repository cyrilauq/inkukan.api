namespace Inkukan.Domain.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken);
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        IQueryable<T> GetQuery();
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
    }
}
