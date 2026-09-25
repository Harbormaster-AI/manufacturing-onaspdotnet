using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IForecastLineRepository
{
    Task<ForecastLine?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ForecastLine>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ForecastLine forecastLine, CancellationToken cancellationToken);
    Task UpdateAsync(ForecastLine forecastLine, CancellationToken cancellationToken);
    Task DeleteAsync(ForecastLine forecastLine, CancellationToken cancellationToken);


}
