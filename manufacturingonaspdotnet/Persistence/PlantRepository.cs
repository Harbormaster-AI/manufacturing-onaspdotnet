
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class PlantRepository : IPlantRepository
{
    private readonly ApplicationDbContext _db;

    public PlantRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Plant?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Plants
            .Include(x => x.Enterprise)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Plant>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Plants
            .AsNoTracking()
            .Include(x => x.Enterprise)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Plant plant, CancellationToken cancellationToken)
    {
        _db.Plants.Add(plant);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Plant plant, CancellationToken cancellationToken)
    {
        _db.Plants.Update(plant);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Plant plant, CancellationToken cancellationToken)
    {
        _db.Plants.Remove(plant);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToProductionLinesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ProductionLines
            .Where(productionLine =>
                request.ChildIds.Contains(productionLine.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    productionLine =>
                        EF.Property<Guid?>(
                            productionLine,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromProductionLinesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ProductionLines
            .Where(productionLine =>
                request.ChildIds.Contains(productionLine.Id) &&
                EF.Property<Guid?>(
                    productionLine,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    productionLine =>
                        EF.Property<Guid?>(
                            productionLine,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }


    public async Task AddToWorkCentersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.WorkCenters
            .Where(workCenter =>
                request.ChildIds.Contains(workCenter.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    workCenter =>
                        EF.Property<Guid?>(
                            workCenter,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromWorkCentersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.WorkCenters
            .Where(workCenter =>
                request.ChildIds.Contains(workCenter.Id) &&
                EF.Property<Guid?>(
                    workCenter,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    workCenter =>
                        EF.Property<Guid?>(
                            workCenter,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }


    public async Task AddToWarehousesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Warehouses
            .Where(warehouse =>
                request.ChildIds.Contains(warehouse.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    warehouse =>
                        EF.Property<Guid?>(
                            warehouse,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromWarehousesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Warehouses
            .Where(warehouse =>
                request.ChildIds.Contains(warehouse.Id) &&
                EF.Property<Guid?>(
                    warehouse,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    warehouse =>
                        EF.Property<Guid?>(
                            warehouse,
                            "PlannedOrder_Id"),
                    (Guid?)null));
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


    public async Task AddToProductionSchedulesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ProductionSchedules
            .Where(productionSchedule =>
                request.ChildIds.Contains(productionSchedule.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    productionSchedule =>
                        EF.Property<Guid?>(
                            productionSchedule,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromProductionSchedulesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ProductionSchedules
            .Where(productionSchedule =>
                request.ChildIds.Contains(productionSchedule.Id) &&
                EF.Property<Guid?>(
                    productionSchedule,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    productionSchedule =>
                        EF.Property<Guid?>(
                            productionSchedule,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }

}
