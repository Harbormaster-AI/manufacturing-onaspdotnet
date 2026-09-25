
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class InspectionResultRepository : IInspectionResultRepository
{
    private readonly ApplicationDbContext _db;

    public InspectionResultRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<InspectionResult?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.InspectionResults
            .Include(x => x.InspectionLot)
            .Include(x => x.Characteristic)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<InspectionResult>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.InspectionResults
            .AsNoTracking()
            .Include(x => x.InspectionLot)
            .Include(x => x.Characteristic)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(InspectionResult inspectionResult, CancellationToken cancellationToken)
    {
        _db.InspectionResults.Add(inspectionResult);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(InspectionResult inspectionResult, CancellationToken cancellationToken)
    {
        _db.InspectionResults.Update(inspectionResult);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(InspectionResult inspectionResult, CancellationToken cancellationToken)
    {
        _db.InspectionResults.Remove(inspectionResult);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
