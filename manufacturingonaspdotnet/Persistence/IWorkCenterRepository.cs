using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IWorkCenterRepository
{
    Task<WorkCenter?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<WorkCenter>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(WorkCenter workCenter, CancellationToken cancellationToken);
    Task UpdateAsync(WorkCenter workCenter, CancellationToken cancellationToken);
    Task DeleteAsync(WorkCenter workCenter, CancellationToken cancellationToken);

    Task AddToAssetsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAssetsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToMaintenanceOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromMaintenanceOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
