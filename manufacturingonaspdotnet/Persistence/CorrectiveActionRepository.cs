
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class CorrectiveActionRepository : ICorrectiveActionRepository
{
    private readonly ApplicationDbContext _db;

    public CorrectiveActionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<CorrectiveAction?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.CorrectiveActions
            .Include(x => x.Nonconformance)
            .Include(x => x.Owner)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CorrectiveAction>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.CorrectiveActions
            .AsNoTracking()
            .Include(x => x.Nonconformance)
            .Include(x => x.Owner)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CorrectiveAction correctiveAction, CancellationToken cancellationToken)
    {
        _db.CorrectiveActions.Add(correctiveAction);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CorrectiveAction correctiveAction, CancellationToken cancellationToken)
    {
        _db.CorrectiveActions.Update(correctiveAction);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CorrectiveAction correctiveAction, CancellationToken cancellationToken)
    {
        _db.CorrectiveActions.Remove(correctiveAction);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
