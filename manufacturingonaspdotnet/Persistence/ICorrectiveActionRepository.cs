using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface ICorrectiveActionRepository
{
    Task<CorrectiveAction?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CorrectiveAction>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(CorrectiveAction correctiveAction, CancellationToken cancellationToken);
    Task UpdateAsync(CorrectiveAction correctiveAction, CancellationToken cancellationToken);
    Task DeleteAsync(CorrectiveAction correctiveAction, CancellationToken cancellationToken);


}
