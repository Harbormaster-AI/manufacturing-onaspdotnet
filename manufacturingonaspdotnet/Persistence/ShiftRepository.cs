
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class ShiftRepository : IShiftRepository
{
    private readonly ApplicationDbContext _db;

    public ShiftRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Shift?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Shifts
            .Include(x => x.Plant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Shift>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Shifts
            .AsNoTracking()
            .Include(x => x.Plant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Shift shift, CancellationToken cancellationToken)
    {
        _db.Shifts.Add(shift);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Shift shift, CancellationToken cancellationToken)
    {
        _db.Shifts.Update(shift);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Shift shift, CancellationToken cancellationToken)
    {
        _db.Shifts.Remove(shift);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToAssignmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ShiftAssignments
            .Where(shiftAssignment =>
                request.ChildIds.Contains(shiftAssignment.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    shiftAssignment =>
                        EF.Property<Guid?>(
                            shiftAssignment,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAssignmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ShiftAssignments
            .Where(shiftAssignment =>
                request.ChildIds.Contains(shiftAssignment.Id) &&
                EF.Property<Guid?>(
                    shiftAssignment,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    shiftAssignment =>
                        EF.Property<Guid?>(
                            shiftAssignment,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }

}
