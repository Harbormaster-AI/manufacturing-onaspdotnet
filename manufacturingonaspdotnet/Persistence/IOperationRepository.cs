using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IOperationRepository
{
    Task<Operation?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Operation>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Operation operation, CancellationToken cancellationToken);
    Task UpdateAsync(Operation operation, CancellationToken cancellationToken);
    Task DeleteAsync(Operation operation, CancellationToken cancellationToken);


}
