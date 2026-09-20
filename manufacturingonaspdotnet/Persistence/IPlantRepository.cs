using manufacturingonaspdotnet.Domain;

namespace manufacturingonaspdotnet.Persistence;

public interface IPlantRepository
{
    Task<Plant?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Plant>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Plant plant, CancellationToken cancellationToken);
    Task UpdateAsync(Plant plant, CancellationToken cancellationToken);
    Task DeleteAsync(Plant plant, CancellationToken cancellationToken);
}
