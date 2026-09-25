
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class MRPRunRepository : IMRPRunRepository
{
    private readonly ApplicationDbContext _db;

    public MRPRunRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<MRPRun?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.MRPRuns
            .Include(x => x.Plant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<MRPRun>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.MRPRuns
            .AsNoTracking()
            .Include(x => x.Plant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(MRPRun mRPRun, CancellationToken cancellationToken)
    {
        _db.MRPRuns.Add(mRPRun);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(MRPRun mRPRun, CancellationToken cancellationToken)
    {
        _db.MRPRuns.Update(mRPRun);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MRPRun mRPRun, CancellationToken cancellationToken)
    {
        _db.MRPRuns.Remove(mRPRun);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToPlannedOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PlannedOrders
            .Where(plannedOrder =>
                request.ChildIds.Contains(plannedOrder.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    plannedOrder =>
                        EF.Property<Guid?>(
                            plannedOrder,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPlannedOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PlannedOrders
            .Where(plannedOrder =>
                request.ChildIds.Contains(plannedOrder.Id) &&
                EF.Property<Guid?>(
                    plannedOrder,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    plannedOrder =>
                        EF.Property<Guid?>(
                            plannedOrder,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }

}
