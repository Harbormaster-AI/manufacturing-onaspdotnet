using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IPurchaseOrderLineRepository
{
    Task<PurchaseOrderLine?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<PurchaseOrderLine>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(PurchaseOrderLine purchaseOrderLine, CancellationToken cancellationToken);
    Task UpdateAsync(PurchaseOrderLine purchaseOrderLine, CancellationToken cancellationToken);
    Task DeleteAsync(PurchaseOrderLine purchaseOrderLine, CancellationToken cancellationToken);


}
