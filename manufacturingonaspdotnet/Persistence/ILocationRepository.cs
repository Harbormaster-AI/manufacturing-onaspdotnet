using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface ILocationRepository
{
    Task<Location?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Location>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Location location, CancellationToken cancellationToken);
    Task UpdateAsync(Location location, CancellationToken cancellationToken);
    Task DeleteAsync(Location location, CancellationToken cancellationToken);

    Task AddToInventoryItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromInventoryItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
