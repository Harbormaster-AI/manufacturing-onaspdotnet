
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class ForecastLineRepository : IForecastLineRepository
{
    private readonly ApplicationDbContext _db;

    public ForecastLineRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ForecastLine?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ForecastLines
            .Include(x => x.Forecast)
            .Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ForecastLine>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ForecastLines
            .AsNoTracking()
            .Include(x => x.Forecast)
            .Include(x => x.Item)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ForecastLine forecastLine, CancellationToken cancellationToken)
    {
        _db.ForecastLines.Add(forecastLine);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ForecastLine forecastLine, CancellationToken cancellationToken)
    {
        _db.ForecastLines.Update(forecastLine);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ForecastLine forecastLine, CancellationToken cancellationToken)
    {
        _db.ForecastLines.Remove(forecastLine);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
