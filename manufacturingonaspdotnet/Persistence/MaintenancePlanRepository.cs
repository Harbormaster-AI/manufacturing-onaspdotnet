using manufacturingonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class MaintenancePlanRepository : IMaintenancePlanRepository
{
    private readonly ApplicationDbContext _db;

    public MaintenancePlanRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<MaintenancePlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.MaintenancePlans
            .Include(x => x.Asset)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<MaintenancePlan>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.MaintenancePlans
            .AsNoTracking()
            .Include(x => x.Asset)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(MaintenancePlan maintenancePlan, CancellationToken cancellationToken)
    {
        _db.MaintenancePlans.Add(maintenancePlan);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(MaintenancePlan maintenancePlan, CancellationToken cancellationToken)
    {
        _db.MaintenancePlans.Update(maintenancePlan);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MaintenancePlan maintenancePlan, CancellationToken cancellationToken)
    {
        _db.MaintenancePlans.Remove(maintenancePlan);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
