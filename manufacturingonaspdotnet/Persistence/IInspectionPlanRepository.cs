using manufacturingonaspdotnet.Domain;

namespace manufacturingonaspdotnet.Persistence;

public interface IInspectionPlanRepository
{
    Task<InspectionPlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<InspectionPlan>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(InspectionPlan inspectionPlan, CancellationToken cancellationToken);
    Task UpdateAsync(InspectionPlan inspectionPlan, CancellationToken cancellationToken);
    Task DeleteAsync(InspectionPlan inspectionPlan, CancellationToken cancellationToken);
}
