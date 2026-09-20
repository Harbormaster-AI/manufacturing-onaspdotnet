using manufacturingonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class InspectionPlanRepository : IInspectionPlanRepository
{
    private readonly ApplicationDbContext _db;

    public InspectionPlanRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<InspectionPlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.InspectionPlans
            .Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<InspectionPlan>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.InspectionPlans
            .AsNoTracking()
            .Include(x => x.Item)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(InspectionPlan inspectionPlan, CancellationToken cancellationToken)
    {
        _db.InspectionPlans.Add(inspectionPlan);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(InspectionPlan inspectionPlan, CancellationToken cancellationToken)
    {
        _db.InspectionPlans.Update(inspectionPlan);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(InspectionPlan inspectionPlan, CancellationToken cancellationToken)
    {
        _db.InspectionPlans.Remove(inspectionPlan);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
