
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class ForecastRepository : IForecastRepository
{
    private readonly ApplicationDbContext _db;

    public ForecastRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Forecast?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Forecasts
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Forecast>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Forecasts
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Forecast forecast, CancellationToken cancellationToken)
    {
        _db.Forecasts.Add(forecast);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Forecast forecast, CancellationToken cancellationToken)
    {
        _db.Forecasts.Update(forecast);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Forecast forecast, CancellationToken cancellationToken)
    {
        _db.Forecasts.Remove(forecast);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToLinesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ForecastLines
            .Where(forecastLine =>
                request.ChildIds.Contains(forecastLine.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    forecastLine =>
                        EF.Property<Guid?>(
                            forecastLine,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromLinesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ForecastLines
            .Where(forecastLine =>
                request.ChildIds.Contains(forecastLine.Id) &&
                EF.Property<Guid?>(
                    forecastLine,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    forecastLine =>
                        EF.Property<Guid?>(
                            forecastLine,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }

}
