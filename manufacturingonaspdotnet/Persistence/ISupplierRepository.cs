using manufacturingonaspdotnet.Domain;

namespace manufacturingonaspdotnet.Persistence;

public interface ISupplierRepository
{
    Task<Supplier?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Supplier>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Supplier supplier, CancellationToken cancellationToken);
    Task UpdateAsync(Supplier supplier, CancellationToken cancellationToken);
    Task DeleteAsync(Supplier supplier, CancellationToken cancellationToken);
}
