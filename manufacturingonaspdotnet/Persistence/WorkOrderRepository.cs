
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class WorkOrderRepository : IWorkOrderRepository
{
    private readonly ApplicationDbContext _db;

    public WorkOrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<WorkOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.WorkOrders
            .Include(x => x.Item)
            .Include(x => x.Plant)
            .Include(x => x.Routing)
            .Include(x => x.Bom)
            .Include(x => x.ProductionSchedule)
            .Include(x => x.SalesOrder)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<WorkOrder>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.WorkOrders
            .AsNoTracking()
            .Include(x => x.Item)
            .Include(x => x.Plant)
            .Include(x => x.Routing)
            .Include(x => x.Bom)
            .Include(x => x.ProductionSchedule)
            .Include(x => x.SalesOrder)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(WorkOrder workOrder, CancellationToken cancellationToken)
    {
        _db.WorkOrders.Add(workOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(WorkOrder workOrder, CancellationToken cancellationToken)
    {
        _db.WorkOrders.Update(workOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(WorkOrder workOrder, CancellationToken cancellationToken)
    {
        _db.WorkOrders.Remove(workOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
