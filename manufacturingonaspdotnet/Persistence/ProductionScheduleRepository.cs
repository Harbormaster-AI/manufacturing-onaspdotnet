
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class ProductionScheduleRepository : IProductionScheduleRepository
{
    private readonly ApplicationDbContext _db;

    public ProductionScheduleRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ProductionSchedule?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ProductionSchedules
            .Include(x => x.Plant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ProductionSchedule>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ProductionSchedules
            .AsNoTracking()
            .Include(x => x.Plant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ProductionSchedule productionSchedule, CancellationToken cancellationToken)
    {
        _db.ProductionSchedules.Add(productionSchedule);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ProductionSchedule productionSchedule, CancellationToken cancellationToken)
    {
        _db.ProductionSchedules.Update(productionSchedule);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ProductionSchedule productionSchedule, CancellationToken cancellationToken)
    {
        _db.ProductionSchedules.Remove(productionSchedule);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToWorkOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.WorkOrders
            .Where(workOrder =>
                request.ChildIds.Contains(workOrder.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    workOrder =>
                        EF.Property<Guid?>(
                            workOrder,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromWorkOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.WorkOrders
            .Where(workOrder =>
                request.ChildIds.Contains(workOrder.Id) &&
                EF.Property<Guid?>(
                    workOrder,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    workOrder =>
                        EF.Property<Guid?>(
                            workOrder,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }

}
