
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class AssetRepository : IAssetRepository
{
    private readonly ApplicationDbContext _db;

    public AssetRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Asset?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Assets
            .Include(x => x.Plant)
            .Include(x => x.WorkCenter)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Asset>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Assets
            .AsNoTracking()
            .Include(x => x.Plant)
            .Include(x => x.WorkCenter)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Asset asset, CancellationToken cancellationToken)
    {
        _db.Assets.Add(asset);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Asset asset, CancellationToken cancellationToken)
    {
        _db.Assets.Update(asset);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Asset asset, CancellationToken cancellationToken)
    {
        _db.Assets.Remove(asset);
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


    public async Task AddToMaintenancePlansAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.MaintenancePlans
            .Where(maintenancePlan =>
                request.ChildIds.Contains(maintenancePlan.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    maintenancePlan =>
                        EF.Property<Guid?>(
                            maintenancePlan,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromMaintenancePlansAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.MaintenancePlans
            .Where(maintenancePlan =>
                request.ChildIds.Contains(maintenancePlan.Id) &&
                EF.Property<Guid?>(
                    maintenancePlan,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    maintenancePlan =>
                        EF.Property<Guid?>(
                            maintenancePlan,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }

}
