
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class PlannedOrderRepository : IPlannedOrderRepository
{
    private readonly ApplicationDbContext _db;

    public PlannedOrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PlannedOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.PlannedOrders
            .Include(x => x.MrpRun)
            .Include(x => x.Item)
            .Include(x => x.Plant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PlannedOrder>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.PlannedOrders
            .AsNoTracking()
            .Include(x => x.MrpRun)
            .Include(x => x.Item)
            .Include(x => x.Plant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PlannedOrder plannedOrder, CancellationToken cancellationToken)
    {
        _db.PlannedOrders.Add(plannedOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PlannedOrder plannedOrder, CancellationToken cancellationToken)
    {
        _db.PlannedOrders.Update(plannedOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PlannedOrder plannedOrder, CancellationToken cancellationToken)
    {
        _db.PlannedOrders.Remove(plannedOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
