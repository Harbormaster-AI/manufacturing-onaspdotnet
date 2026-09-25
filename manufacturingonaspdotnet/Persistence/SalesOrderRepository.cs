
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class SalesOrderRepository : ISalesOrderRepository
{
    private readonly ApplicationDbContext _db;

    public SalesOrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<SalesOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.SalesOrders
            .Include(x => x.Customer)
            .Include(x => x.Plant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<SalesOrder>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.SalesOrders
            .AsNoTracking()
            .Include(x => x.Customer)
            .Include(x => x.Plant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(SalesOrder salesOrder, CancellationToken cancellationToken)
    {
        _db.SalesOrders.Add(salesOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(SalesOrder salesOrder, CancellationToken cancellationToken)
    {
        _db.SalesOrders.Update(salesOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(SalesOrder salesOrder, CancellationToken cancellationToken)
    {
        _db.SalesOrders.Remove(salesOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToLinesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.SalesOrderLines
            .Where(salesOrderLine =>
                request.ChildIds.Contains(salesOrderLine.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    salesOrderLine =>
                        EF.Property<Guid?>(
                            salesOrderLine,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromLinesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.SalesOrderLines
            .Where(salesOrderLine =>
                request.ChildIds.Contains(salesOrderLine.Id) &&
                EF.Property<Guid?>(
                    salesOrderLine,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    salesOrderLine =>
                        EF.Property<Guid?>(
                            salesOrderLine,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }


    public async Task AddToWorkOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.WorkOrders
            .Where(workOrder =>
                request.ChildIds.Contains(workOrder.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    workOrder =>
                        EF.Property<Guid?>(
                            workOrder,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromWorkOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.WorkOrders
            .Where(workOrder =>
                request.ChildIds.Contains(workOrder.Id) &&
                EF.Property<Guid?>(
                    workOrder,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    workOrder =>
                        EF.Property<Guid?>(
                            workOrder,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }

}
