
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class GoodsReceiptRepository : IGoodsReceiptRepository
{
    private readonly ApplicationDbContext _db;

    public GoodsReceiptRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<GoodsReceipt?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.GoodsReceipts
            .Include(x => x.PurchaseOrder)
            .Include(x => x.Warehouse)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<GoodsReceipt>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.GoodsReceipts
            .AsNoTracking()
            .Include(x => x.PurchaseOrder)
            .Include(x => x.Warehouse)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(GoodsReceipt goodsReceipt, CancellationToken cancellationToken)
    {
        _db.GoodsReceipts.Add(goodsReceipt);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(GoodsReceipt goodsReceipt, CancellationToken cancellationToken)
    {
        _db.GoodsReceipts.Update(goodsReceipt);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(GoodsReceipt goodsReceipt, CancellationToken cancellationToken)
    {
        _db.GoodsReceipts.Remove(goodsReceipt);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToLinesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.GoodsReceiptLines
            .Where(goodsReceiptLine =>
                request.ChildIds.Contains(goodsReceiptLine.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    goodsReceiptLine =>
                        EF.Property<Guid?>(
                            goodsReceiptLine,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromLinesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.GoodsReceiptLines
            .Where(goodsReceiptLine =>
                request.ChildIds.Contains(goodsReceiptLine.Id) &&
                EF.Property<Guid?>(
                    goodsReceiptLine,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    goodsReceiptLine =>
                        EF.Property<Guid?>(
                            goodsReceiptLine,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }

}
