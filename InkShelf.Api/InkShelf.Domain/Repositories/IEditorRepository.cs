using InkShelf.Domain.Entities;

namespace InkShelf.Domain.Repositories
{
    public interface IEditorRepository : IBaseRepository<Editor>
    {
        Task<Editor?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
