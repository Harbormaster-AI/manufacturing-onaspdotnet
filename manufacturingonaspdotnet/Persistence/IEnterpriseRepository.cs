using manufacturingonaspdotnet.Domain;

namespace manufacturingonaspdotnet.Persistence;

public interface IEnterpriseRepository
{
    Task<Enterprise?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Enterprise>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Enterprise enterprise, CancellationToken cancellationToken);
    Task UpdateAsync(Enterprise enterprise, CancellationToken cancellationToken);
    Task DeleteAsync(Enterprise enterprise, CancellationToken cancellationToken);
}
