using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IRoutingRepository
{
    Task<Routing?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Routing>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Routing routing, CancellationToken cancellationToken);
    Task UpdateAsync(Routing routing, CancellationToken cancellationToken);
    Task DeleteAsync(Routing routing, CancellationToken cancellationToken);

    Task AddToOperationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromOperationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
