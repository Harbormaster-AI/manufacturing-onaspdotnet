
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class EnterpriseRepository : IEnterpriseRepository
{
    private readonly ApplicationDbContext _db;

    public EnterpriseRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Enterprise?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Enterprises
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Enterprise>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Enterprises
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Enterprise enterprise, CancellationToken cancellationToken)
    {
        _db.Enterprises.Add(enterprise);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Enterprise enterprise, CancellationToken cancellationToken)
    {
        _db.Enterprises.Update(enterprise);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Enterprise enterprise, CancellationToken cancellationToken)
    {
        _db.Enterprises.Remove(enterprise);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToBusinessUnitsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.BusinessUnits
            .Where(businessUnit =>
                request.ChildIds.Contains(businessUnit.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    businessUnit =>
                        EF.Property<Guid?>(
                            businessUnit,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromBusinessUnitsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.BusinessUnits
            .Where(businessUnit =>
                request.ChildIds.Contains(businessUnit.Id) &&
                EF.Property<Guid?>(
                    businessUnit,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    businessUnit =>
                        EF.Property<Guid?>(
                            businessUnit,
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


    public async Task AddToCustomersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Customers
            .Where(customer =>
                request.ChildIds.Contains(customer.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    customer =>
                        EF.Property<Guid?>(
                            customer,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCustomersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Customers
            .Where(customer =>
                request.ChildIds.Contains(customer.Id) &&
                EF.Property<Guid?>(
                    customer,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    customer =>
                        EF.Property<Guid?>(
                            customer,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }

}
