using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IPlannedOrderRepository
{
    Task<PlannedOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<PlannedOrder>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(PlannedOrder plannedOrder, CancellationToken cancellationToken);
    Task UpdateAsync(PlannedOrder plannedOrder, CancellationToken cancellationToken);
    Task DeleteAsync(PlannedOrder plannedOrder, CancellationToken cancellationToken);


}
