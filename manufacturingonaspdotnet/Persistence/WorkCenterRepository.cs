
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class WorkCenterRepository : IWorkCenterRepository
{
    private readonly ApplicationDbContext _db;

    public WorkCenterRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<WorkCenter?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.WorkCenters
            .Include(x => x.ProductionLine)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<WorkCenter>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.WorkCenters
            .AsNoTracking()
            .Include(x => x.ProductionLine)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(WorkCenter workCenter, CancellationToken cancellationToken)
    {
        _db.WorkCenters.Add(workCenter);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(WorkCenter workCenter, CancellationToken cancellationToken)
    {
        _db.WorkCenters.Update(workCenter);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(WorkCenter workCenter, CancellationToken cancellationToken)
    {
        _db.WorkCenters.Remove(workCenter);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToAssetsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Assets
            .Where(asset =>
                request.ChildIds.Contains(asset.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    asset =>
                        EF.Property<Guid?>(
                            asset,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAssetsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Assets
            .Where(asset =>
                request.ChildIds.Contains(asset.Id) &&
                EF.Property<Guid?>(
                    asset,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    asset =>
                        EF.Property<Guid?>(
                            asset,
                            "PlannedOrder_Id"),
                    (Guid?)null));
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
