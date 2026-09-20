using manufacturingonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class BusinessUnitRepository : IBusinessUnitRepository
{
    private readonly ApplicationDbContext _db;

    public BusinessUnitRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<BusinessUnit?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.BusinessUnits
            .Include(x => x.Enterprise)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<BusinessUnit>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.BusinessUnits
            .AsNoTracking()
            .Include(x => x.Enterprise)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(BusinessUnit businessUnit, CancellationToken cancellationToken)
    {
        _db.BusinessUnits.Add(businessUnit);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(BusinessUnit businessUnit, CancellationToken cancellationToken)
    {
        _db.BusinessUnits.Update(businessUnit);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(BusinessUnit businessUnit, CancellationToken cancellationToken)
    {
        _db.BusinessUnits.Remove(businessUnit);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
