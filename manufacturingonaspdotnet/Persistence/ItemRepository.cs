
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class ItemRepository : IItemRepository
{
    private readonly ApplicationDbContext _db;

    public ItemRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Item?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Items
            .Include(x => x.BusinessUnit)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Item>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Items
            .AsNoTracking()
            .Include(x => x.BusinessUnit)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Item item, CancellationToken cancellationToken)
    {
        _db.Items.Add(item);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Item item, CancellationToken cancellationToken)
    {
        _db.Items.Update(item);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Item item, CancellationToken cancellationToken)
    {
        _db.Items.Remove(item);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToBomsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.BOMs
            .Where(bOM =>
                request.ChildIds.Contains(bOM.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    bOM =>
                        EF.Property<Guid?>(
                            bOM,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromBomsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.BOMs
            .Where(bOM =>
                request.ChildIds.Contains(bOM.Id) &&
                EF.Property<Guid?>(
                    bOM,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    bOM =>
                        EF.Property<Guid?>(
                            bOM,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }


    public async Task AddToRoutingsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Routings
            .Where(routing =>
                request.ChildIds.Contains(routing.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    routing =>
                        EF.Property<Guid?>(
                            routing,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromRoutingsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Routings
            .Where(routing =>
                request.ChildIds.Contains(routing.Id) &&
                EF.Property<Guid?>(
                    routing,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    routing =>
                        EF.Property<Guid?>(
                            routing,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }


    public async Task AddToSuppliersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Suppliers
            .Where(supplier =>
                request.ChildIds.Contains(supplier.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    supplier =>
                        EF.Property<Guid?>(
                            supplier,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromSuppliersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Suppliers
            .Where(supplier =>
                request.ChildIds.Contains(supplier.Id) &&
                EF.Property<Guid?>(
                    supplier,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    supplier =>
                        EF.Property<Guid?>(
                            supplier,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }


    public async Task AddToQualitySpecificationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.QualitySpecifications
            .Where(qualitySpecification =>
                request.ChildIds.Contains(qualitySpecification.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    qualitySpecification =>
                        EF.Property<Guid?>(
                            qualitySpecification,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromQualitySpecificationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.QualitySpecifications
            .Where(qualitySpecification =>
                request.ChildIds.Contains(qualitySpecification.Id) &&
                EF.Property<Guid?>(
                    qualitySpecification,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    qualitySpecification =>
                        EF.Property<Guid?>(
                            qualitySpecification,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }


    public async Task AddToInventoryItemsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.InventoryItems
            .Where(inventoryItem =>
                request.ChildIds.Contains(inventoryItem.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    inventoryItem =>
                        EF.Property<Guid?>(
                            inventoryItem,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromInventoryItemsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.InventoryItems
            .Where(inventoryItem =>
                request.ChildIds.Contains(inventoryItem.Id) &&
                EF.Property<Guid?>(
                    inventoryItem,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    inventoryItem =>
                        EF.Property<Guid?>(
                            inventoryItem,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }

}
