using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IBusinessUnitRepository
{
    Task<BusinessUnit?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<BusinessUnit>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(BusinessUnit businessUnit, CancellationToken cancellationToken);
    Task UpdateAsync(BusinessUnit businessUnit, CancellationToken cancellationToken);
    Task DeleteAsync(BusinessUnit businessUnit, CancellationToken cancellationToken);

    Task AddToItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPlantsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPlantsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
