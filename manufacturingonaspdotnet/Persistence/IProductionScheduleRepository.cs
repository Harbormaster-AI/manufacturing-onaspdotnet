using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IProductionScheduleRepository
{
    Task<ProductionSchedule?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ProductionSchedule>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ProductionSchedule productionSchedule, CancellationToken cancellationToken);
    Task UpdateAsync(ProductionSchedule productionSchedule, CancellationToken cancellationToken);
    Task DeleteAsync(ProductionSchedule productionSchedule, CancellationToken cancellationToken);

    Task AddToWorkOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromWorkOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
