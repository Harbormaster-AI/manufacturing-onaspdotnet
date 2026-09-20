using manufacturingonaspdotnet.Domain;

namespace manufacturingonaspdotnet.Persistence;

public interface IBusinessUnitRepository
{
    Task<BusinessUnit?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<BusinessUnit>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(BusinessUnit businessUnit, CancellationToken cancellationToken);
    Task UpdateAsync(BusinessUnit businessUnit, CancellationToken cancellationToken);
    Task DeleteAsync(BusinessUnit businessUnit, CancellationToken cancellationToken);
}
