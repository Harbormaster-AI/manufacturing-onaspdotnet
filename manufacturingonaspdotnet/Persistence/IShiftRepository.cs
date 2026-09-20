using manufacturingonaspdotnet.Domain;

namespace manufacturingonaspdotnet.Persistence;

public interface IShiftRepository
{
    Task<Shift?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Shift>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Shift shift, CancellationToken cancellationToken);
    Task UpdateAsync(Shift shift, CancellationToken cancellationToken);
    Task DeleteAsync(Shift shift, CancellationToken cancellationToken);
}
