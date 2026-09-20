using manufacturingonaspdotnet.Domain;

namespace manufacturingonaspdotnet.Persistence;

public interface IProductionScheduleRepository
{
    Task<ProductionSchedule?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ProductionSchedule>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ProductionSchedule productionSchedule, CancellationToken cancellationToken);
    Task UpdateAsync(ProductionSchedule productionSchedule, CancellationToken cancellationToken);
    Task DeleteAsync(ProductionSchedule productionSchedule, CancellationToken cancellationToken);
}
