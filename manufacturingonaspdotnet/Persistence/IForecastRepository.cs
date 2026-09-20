using manufacturingonaspdotnet.Domain;

namespace manufacturingonaspdotnet.Persistence;

public interface IForecastRepository
{
    Task<Forecast?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Forecast>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Forecast forecast, CancellationToken cancellationToken);
    Task UpdateAsync(Forecast forecast, CancellationToken cancellationToken);
    Task DeleteAsync(Forecast forecast, CancellationToken cancellationToken);
}
