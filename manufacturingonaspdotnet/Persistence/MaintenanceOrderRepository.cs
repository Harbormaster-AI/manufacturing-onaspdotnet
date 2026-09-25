
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class MaintenanceOrderRepository : IMaintenanceOrderRepository
{
    private readonly ApplicationDbContext _db;

    public MaintenanceOrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<MaintenanceOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.MaintenanceOrders
            .Include(x => x.Asset)
            .Include(x => x.Plan)
            .Include(x => x.WorkCenter)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<MaintenanceOrder>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.MaintenanceOrders
            .AsNoTracking()
            .Include(x => x.Asset)
            .Include(x => x.Plan)
            .Include(x => x.WorkCenter)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(MaintenanceOrder maintenanceOrder, CancellationToken cancellationToken)
    {
        _db.MaintenanceOrders.Add(maintenanceOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(MaintenanceOrder maintenanceOrder, CancellationToken cancellationToken)
    {
        _db.MaintenanceOrders.Update(maintenanceOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MaintenanceOrder maintenanceOrder, CancellationToken cancellationToken)
    {
        _db.MaintenanceOrders.Remove(maintenanceOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
