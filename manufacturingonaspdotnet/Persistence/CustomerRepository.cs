
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _db;

    public CustomerRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Customers
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Customers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Customer customer, CancellationToken cancellationToken)
    {
        _db.Customers.Add(customer);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Customer customer, CancellationToken cancellationToken)
    {
        _db.Customers.Update(customer);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Customer customer, CancellationToken cancellationToken)
    {
        _db.Customers.Remove(customer);
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


    public async Task AddToSalesOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.SalesOrders
            .Where(salesOrder =>
                request.ChildIds.Contains(salesOrder.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    salesOrder =>
                        EF.Property<Guid?>(
                            salesOrder,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromSalesOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.SalesOrders
            .Where(salesOrder =>
                request.ChildIds.Contains(salesOrder.Id) &&
                EF.Property<Guid?>(
                    salesOrder,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    salesOrder =>
                        EF.Property<Guid?>(
                            salesOrder,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }

}
