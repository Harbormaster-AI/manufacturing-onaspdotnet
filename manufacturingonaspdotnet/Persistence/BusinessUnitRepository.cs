
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class BusinessUnitRepository : IBusinessUnitRepository
{
    private readonly ApplicationDbContext _db;

    public BusinessUnitRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<BusinessUnit?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.BusinessUnits
            .Include(x => x.Enterprise)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<BusinessUnit>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.BusinessUnits
            .AsNoTracking()
            .Include(x => x.Enterprise)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(BusinessUnit businessUnit, CancellationToken cancellationToken)
    {
        _db.BusinessUnits.Add(businessUnit);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(BusinessUnit businessUnit, CancellationToken cancellationToken)
    {
        _db.BusinessUnits.Update(businessUnit);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(BusinessUnit businessUnit, CancellationToken cancellationToken)
    {
        _db.BusinessUnits.Remove(businessUnit);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToItemsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Items
            .Where(item =>
                request.ChildIds.Contains(item.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    item =>
                        EF.Property<Guid?>(
                            item,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromItemsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Items
            .Where(item =>
                request.ChildIds.Contains(item.Id) &&
                EF.Property<Guid?>(
                    item,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    item =>
                        EF.Property<Guid?>(
                            item,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }


    public async Task AddToPlantsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Plants
            .Where(plant =>
                request.ChildIds.Contains(plant.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    plant =>
                        EF.Property<Guid?>(
                            plant,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPlantsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Plants
            .Where(plant =>
                request.ChildIds.Contains(plant.Id) &&
                EF.Property<Guid?>(
                    plant,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    plant =>
                        EF.Property<Guid?>(
                            plant,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }

}
