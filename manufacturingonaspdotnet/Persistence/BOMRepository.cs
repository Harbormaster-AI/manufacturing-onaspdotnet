
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class BOMRepository : IBOMRepository
{
    private readonly ApplicationDbContext _db;

    public BOMRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<BOM?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.BOMs
            .Include(x => x.ParentItem)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<BOM>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.BOMs
            .AsNoTracking()
            .Include(x => x.ParentItem)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(BOM bOM, CancellationToken cancellationToken)
    {
        _db.BOMs.Add(bOM);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(BOM bOM, CancellationToken cancellationToken)
    {
        _db.BOMs.Update(bOM);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(BOM bOM, CancellationToken cancellationToken)
    {
        _db.BOMs.Remove(bOM);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToBomItemsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.BOMItems
            .Where(bOMItem =>
                request.ChildIds.Contains(bOMItem.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    bOMItem =>
                        EF.Property<Guid?>(
                            bOMItem,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromBomItemsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.BOMItems
            .Where(bOMItem =>
                request.ChildIds.Contains(bOMItem.Id) &&
                EF.Property<Guid?>(
                    bOMItem,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    bOMItem =>
                        EF.Property<Guid?>(
                            bOMItem,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }

}
