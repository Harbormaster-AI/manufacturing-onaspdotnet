using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IMRPRunRepository
{
    Task<MRPRun?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<MRPRun>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(MRPRun mRPRun, CancellationToken cancellationToken);
    Task UpdateAsync(MRPRun mRPRun, CancellationToken cancellationToken);
    Task DeleteAsync(MRPRun mRPRun, CancellationToken cancellationToken);

    Task AddToPlannedOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPlannedOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
