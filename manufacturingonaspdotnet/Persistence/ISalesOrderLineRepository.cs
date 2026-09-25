using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface ISalesOrderLineRepository
{
    Task<SalesOrderLine?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<SalesOrderLine>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(SalesOrderLine salesOrderLine, CancellationToken cancellationToken);
    Task UpdateAsync(SalesOrderLine salesOrderLine, CancellationToken cancellationToken);
    Task DeleteAsync(SalesOrderLine salesOrderLine, CancellationToken cancellationToken);


}
