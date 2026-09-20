using manufacturingonaspdotnet.Domain;

namespace manufacturingonaspdotnet.Persistence;

public interface IMRPRunRepository
{
    Task<MRPRun?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<MRPRun>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(MRPRun mRPRun, CancellationToken cancellationToken);
    Task UpdateAsync(MRPRun mRPRun, CancellationToken cancellationToken);
    Task DeleteAsync(MRPRun mRPRun, CancellationToken cancellationToken);
}
