using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IInspectionPlanRepository
{
    Task<InspectionPlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<InspectionPlan>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(InspectionPlan inspectionPlan, CancellationToken cancellationToken);
    Task UpdateAsync(InspectionPlan inspectionPlan, CancellationToken cancellationToken);
    Task DeleteAsync(InspectionPlan inspectionPlan, CancellationToken cancellationToken);

    Task AddToCharacteristicsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCharacteristicsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
