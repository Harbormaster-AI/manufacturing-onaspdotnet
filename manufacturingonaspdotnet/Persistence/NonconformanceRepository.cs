
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class NonconformanceRepository : INonconformanceRepository
{
    private readonly ApplicationDbContext _db;

    public NonconformanceRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Nonconformance?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Nonconformances
            .Include(x => x.Item)
            .Include(x => x.WorkOrder)
            .Include(x => x.InspectionLot)
            .Include(x => x.CorrectiveAction)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Nonconformance>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Nonconformances
            .AsNoTracking()
            .Include(x => x.Item)
            .Include(x => x.WorkOrder)
            .Include(x => x.InspectionLot)
            .Include(x => x.CorrectiveAction)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Nonconformance nonconformance, CancellationToken cancellationToken)
    {
        _db.Nonconformances.Add(nonconformance);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Nonconformance nonconformance, CancellationToken cancellationToken)
    {
        _db.Nonconformances.Update(nonconformance);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Nonconformance nonconformance, CancellationToken cancellationToken)
    {
        _db.Nonconformances.Remove(nonconformance);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
