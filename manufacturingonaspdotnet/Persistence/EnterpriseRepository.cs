using manufacturingonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class EnterpriseRepository : IEnterpriseRepository
{
    private readonly ApplicationDbContext _db;

    public EnterpriseRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Enterprise?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Enterprises
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Enterprise>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Enterprises
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Enterprise enterprise, CancellationToken cancellationToken)
    {
        _db.Enterprises.Add(enterprise);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Enterprise enterprise, CancellationToken cancellationToken)
    {
        _db.Enterprises.Update(enterprise);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Enterprise enterprise, CancellationToken cancellationToken)
    {
        _db.Enterprises.Remove(enterprise);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
