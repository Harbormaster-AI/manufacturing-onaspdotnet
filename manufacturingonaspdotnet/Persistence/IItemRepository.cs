using manufacturingonaspdotnet.Domain;

namespace manufacturingonaspdotnet.Persistence;

public interface IItemRepository
{
    Task<Item?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Item>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Item item, CancellationToken cancellationToken);
    Task UpdateAsync(Item item, CancellationToken cancellationToken);
    Task DeleteAsync(Item item, CancellationToken cancellationToken);
}
