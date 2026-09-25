using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IWarehouseRepository
{
    Task<Warehouse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Warehouse>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Warehouse warehouse, CancellationToken cancellationToken);
    Task UpdateAsync(Warehouse warehouse, CancellationToken cancellationToken);
    Task DeleteAsync(Warehouse warehouse, CancellationToken cancellationToken);

    Task AddToLocationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromLocationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToInventoryItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromInventoryItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
