using Inkukan.Domain.Entities;

namespace Inkukan.Domain.Repositories
{
    public interface IEditorRepository : IBaseRepository<Editor>
    {
        Task<Editor?> GetByNameAsync(string name, CancellationToken cancellationToken);
    }
}
