using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface ISalesOrderRepository
{
    Task<SalesOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<SalesOrder>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(SalesOrder salesOrder, CancellationToken cancellationToken);
    Task UpdateAsync(SalesOrder salesOrder, CancellationToken cancellationToken);
    Task DeleteAsync(SalesOrder salesOrder, CancellationToken cancellationToken);

    Task AddToLinesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromLinesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToWorkOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromWorkOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
