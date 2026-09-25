using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IBOMItemRepository
{
    Task<BOMItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<BOMItem>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(BOMItem bOMItem, CancellationToken cancellationToken);
    Task UpdateAsync(BOMItem bOMItem, CancellationToken cancellationToken);
    Task DeleteAsync(BOMItem bOMItem, CancellationToken cancellationToken);


}
