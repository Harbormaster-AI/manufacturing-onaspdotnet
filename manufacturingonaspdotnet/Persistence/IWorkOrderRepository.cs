using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IWorkOrderRepository
{
    Task<WorkOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<WorkOrder>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(WorkOrder workOrder, CancellationToken cancellationToken);
    Task UpdateAsync(WorkOrder workOrder, CancellationToken cancellationToken);
    Task DeleteAsync(WorkOrder workOrder, CancellationToken cancellationToken);


}
