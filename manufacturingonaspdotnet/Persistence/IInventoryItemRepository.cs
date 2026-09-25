using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IInventoryItemRepository
{
    Task<InventoryItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<InventoryItem>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(InventoryItem inventoryItem, CancellationToken cancellationToken);
    Task UpdateAsync(InventoryItem inventoryItem, CancellationToken cancellationToken);
    Task DeleteAsync(InventoryItem inventoryItem, CancellationToken cancellationToken);


}
