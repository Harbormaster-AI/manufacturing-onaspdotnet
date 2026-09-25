
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class SupplierRepository : ISupplierRepository
{
    private readonly ApplicationDbContext _db;

    public SupplierRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Supplier?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Suppliers
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Supplier>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Suppliers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Supplier supplier, CancellationToken cancellationToken)
    {
        _db.Suppliers.Add(supplier);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Supplier supplier, CancellationToken cancellationToken)
    {
        _db.Suppliers.Update(supplier);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Supplier supplier, CancellationToken cancellationToken)
    {
        _db.Suppliers.Remove(supplier);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToEnterprisesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Enterprises
            .Where(enterprise =>
                request.ChildIds.Contains(enterprise.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    enterprise =>
                        EF.Property<Guid?>(
                            enterprise,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromEnterprisesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Enterprises
            .Where(enterprise =>
                request.ChildIds.Contains(enterprise.Id) &&
                EF.Property<Guid?>(
                    enterprise,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    enterprise =>
                        EF.Property<Guid?>(
                            enterprise,
                            "PlannedOrder_Id"),
                    (Guid?)null));
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


    public async Task AddToPurchaseOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PurchaseOrders
            .Where(purchaseOrder =>
                request.ChildIds.Contains(purchaseOrder.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    purchaseOrder =>
                        EF.Property<Guid?>(
                            purchaseOrder,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPurchaseOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PurchaseOrders
            .Where(purchaseOrder =>
                request.ChildIds.Contains(purchaseOrder.Id) &&
                EF.Property<Guid?>(
                    purchaseOrder,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    purchaseOrder =>
                        EF.Property<Guid?>(
                            purchaseOrder,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }

}
