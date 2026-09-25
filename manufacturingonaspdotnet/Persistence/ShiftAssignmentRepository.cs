
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class ShiftAssignmentRepository : IShiftAssignmentRepository
{
    private readonly ApplicationDbContext _db;

    public ShiftAssignmentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ShiftAssignment?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ShiftAssignments
            .Include(x => x.Shift)
            .Include(x => x.Employee)
            .Include(x => x.WorkCenter)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ShiftAssignment>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ShiftAssignments
            .AsNoTracking()
            .Include(x => x.Shift)
            .Include(x => x.Employee)
            .Include(x => x.WorkCenter)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ShiftAssignment shiftAssignment, CancellationToken cancellationToken)
    {
        _db.ShiftAssignments.Add(shiftAssignment);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ShiftAssignment shiftAssignment, CancellationToken cancellationToken)
    {
        _db.ShiftAssignments.Update(shiftAssignment);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ShiftAssignment shiftAssignment, CancellationToken cancellationToken)
    {
        _db.ShiftAssignments.Remove(shiftAssignment);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
