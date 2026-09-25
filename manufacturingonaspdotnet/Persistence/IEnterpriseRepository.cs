using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IEnterpriseRepository
{
    Task<Enterprise?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Enterprise>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Enterprise enterprise, CancellationToken cancellationToken);
    Task UpdateAsync(Enterprise enterprise, CancellationToken cancellationToken);
    Task DeleteAsync(Enterprise enterprise, CancellationToken cancellationToken);

    Task AddToBusinessUnitsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromBusinessUnitsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPlantsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPlantsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToSuppliersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromSuppliersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToCustomersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCustomersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
