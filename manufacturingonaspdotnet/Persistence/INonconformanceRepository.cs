using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface INonconformanceRepository
{
    Task<Nonconformance?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Nonconformance>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Nonconformance nonconformance, CancellationToken cancellationToken);
    Task UpdateAsync(Nonconformance nonconformance, CancellationToken cancellationToken);
    Task DeleteAsync(Nonconformance nonconformance, CancellationToken cancellationToken);


}
