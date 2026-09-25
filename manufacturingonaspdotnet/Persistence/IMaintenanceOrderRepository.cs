using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IMaintenanceOrderRepository
{
    Task<MaintenanceOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<MaintenanceOrder>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(MaintenanceOrder maintenanceOrder, CancellationToken cancellationToken);
    Task UpdateAsync(MaintenanceOrder maintenanceOrder, CancellationToken cancellationToken);
    Task DeleteAsync(MaintenanceOrder maintenanceOrder, CancellationToken cancellationToken);


}
