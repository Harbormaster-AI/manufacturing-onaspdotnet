using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IAssetRepository
{
    Task<Asset?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Asset>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Asset asset, CancellationToken cancellationToken);
    Task UpdateAsync(Asset asset, CancellationToken cancellationToken);
    Task DeleteAsync(Asset asset, CancellationToken cancellationToken);

    Task AddToMaintenanceOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromMaintenanceOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToMaintenancePlansAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromMaintenancePlansAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
