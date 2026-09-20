using manufacturingonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class WorkCenterRepository : IWorkCenterRepository
{
    private readonly ApplicationDbContext _db;

    public WorkCenterRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<WorkCenter?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.WorkCenters
            .Include(x => x.ProductionLine)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<WorkCenter>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.WorkCenters
            .AsNoTracking()
            .Include(x => x.ProductionLine)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(WorkCenter workCenter, CancellationToken cancellationToken)
    {
        _db.WorkCenters.Add(workCenter);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(WorkCenter workCenter, CancellationToken cancellationToken)
    {
        _db.WorkCenters.Update(workCenter);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(WorkCenter workCenter, CancellationToken cancellationToken)
    {
        _db.WorkCenters.Remove(workCenter);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
