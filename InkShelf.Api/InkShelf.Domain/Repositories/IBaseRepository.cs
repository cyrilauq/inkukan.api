namespace InkShelf.Domain.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        IQueryable<T> GetQuery();
    }
}
