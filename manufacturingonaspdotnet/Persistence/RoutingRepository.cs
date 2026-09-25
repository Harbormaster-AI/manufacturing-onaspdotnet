
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class RoutingRepository : IRoutingRepository
{
    private readonly ApplicationDbContext _db;

    public RoutingRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Routing?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Routings
            .Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Routing>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Routings
            .AsNoTracking()
            .Include(x => x.Item)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Routing routing, CancellationToken cancellationToken)
    {
        _db.Routings.Add(routing);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Routing routing, CancellationToken cancellationToken)
    {
        _db.Routings.Update(routing);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Routing routing, CancellationToken cancellationToken)
    {
        _db.Routings.Remove(routing);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToOperationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Operations
            .Where(operation =>
                request.ChildIds.Contains(operation.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    operation =>
                        EF.Property<Guid?>(
                            operation,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromOperationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Operations
            .Where(operation =>
                request.ChildIds.Contains(operation.Id) &&
                EF.Property<Guid?>(
                    operation,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    operation =>
                        EF.Property<Guid?>(
                            operation,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }

}
