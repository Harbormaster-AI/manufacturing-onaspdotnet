using manufacturingonaspdotnet.Domain;

namespace manufacturingonaspdotnet.Persistence;

public interface IPurchaseOrderRepository
{
    Task<PurchaseOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<PurchaseOrder>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(PurchaseOrder purchaseOrder, CancellationToken cancellationToken);
    Task UpdateAsync(PurchaseOrder purchaseOrder, CancellationToken cancellationToken);
    Task DeleteAsync(PurchaseOrder purchaseOrder, CancellationToken cancellationToken);
}
