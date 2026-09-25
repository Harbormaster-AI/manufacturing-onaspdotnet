
using manufacturingonaspdotnet.Contracts;
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


    public async Task AddToMaintenanceOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.MaintenanceOrders
            .Where(maintenanceOrder =>
                request.ChildIds.Contains(maintenanceOrder.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    maintenanceOrder =>
                        EF.Property<Guid?>(
                            maintenanceOrder,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromMaintenanceOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.MaintenanceOrders
            .Where(maintenanceOrder =>
                request.ChildIds.Contains(maintenanceOrder.Id) &&
                EF.Property<Guid?>(
                    maintenanceOrder,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    maintenanceOrder =>
                        EF.Property<Guid?>(
                            maintenanceOrder,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }

}
