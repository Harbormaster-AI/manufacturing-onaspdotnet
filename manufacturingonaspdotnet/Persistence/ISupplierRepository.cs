using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface ISupplierRepository
{
    Task<Supplier?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Supplier>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Supplier supplier, CancellationToken cancellationToken);
    Task UpdateAsync(Supplier supplier, CancellationToken cancellationToken);
    Task DeleteAsync(Supplier supplier, CancellationToken cancellationToken);

    Task AddToEnterprisesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromEnterprisesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPurchaseOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPurchaseOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
