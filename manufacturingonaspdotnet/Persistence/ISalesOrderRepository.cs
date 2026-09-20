using manufacturingonaspdotnet.Domain;

namespace manufacturingonaspdotnet.Persistence;

public interface ISalesOrderRepository
{
    Task<SalesOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<SalesOrder>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(SalesOrder salesOrder, CancellationToken cancellationToken);
    Task UpdateAsync(SalesOrder salesOrder, CancellationToken cancellationToken);
    Task DeleteAsync(SalesOrder salesOrder, CancellationToken cancellationToken);
}
