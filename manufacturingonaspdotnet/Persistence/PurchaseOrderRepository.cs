
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class PurchaseOrderRepository : IPurchaseOrderRepository
{
    private readonly ApplicationDbContext _db;

    public PurchaseOrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PurchaseOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.PurchaseOrders
            .Include(x => x.Supplier)
            .Include(x => x.Plant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PurchaseOrder>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.PurchaseOrders
            .AsNoTracking()
            .Include(x => x.Supplier)
            .Include(x => x.Plant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PurchaseOrder purchaseOrder, CancellationToken cancellationToken)
    {
        _db.PurchaseOrders.Add(purchaseOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PurchaseOrder purchaseOrder, CancellationToken cancellationToken)
    {
        _db.PurchaseOrders.Update(purchaseOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PurchaseOrder purchaseOrder, CancellationToken cancellationToken)
    {
        _db.PurchaseOrders.Remove(purchaseOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToLinesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PurchaseOrderLines
            .Where(purchaseOrderLine =>
                request.ChildIds.Contains(purchaseOrderLine.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    purchaseOrderLine =>
                        EF.Property<Guid?>(
                            purchaseOrderLine,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromLinesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PurchaseOrderLines
            .Where(purchaseOrderLine =>
                request.ChildIds.Contains(purchaseOrderLine.Id) &&
                EF.Property<Guid?>(
                    purchaseOrderLine,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    purchaseOrderLine =>
                        EF.Property<Guid?>(
                            purchaseOrderLine,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }


    public async Task AddToGoodsReceiptsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.GoodsReceipts
            .Where(goodsReceipt =>
                request.ChildIds.Contains(goodsReceipt.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    goodsReceipt =>
                        EF.Property<Guid?>(
                            goodsReceipt,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromGoodsReceiptsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.GoodsReceipts
            .Where(goodsReceipt =>
                request.ChildIds.Contains(goodsReceipt.Id) &&
                EF.Property<Guid?>(
                    goodsReceipt,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    goodsReceipt =>
                        EF.Property<Guid?>(
                            goodsReceipt,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }

}
