using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IMaintenancePlanRepository
{
    Task<MaintenancePlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<MaintenancePlan>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(MaintenancePlan maintenancePlan, CancellationToken cancellationToken);
    Task UpdateAsync(MaintenancePlan maintenancePlan, CancellationToken cancellationToken);
    Task DeleteAsync(MaintenancePlan maintenancePlan, CancellationToken cancellationToken);

    Task AddToMaintenanceOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromMaintenanceOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
