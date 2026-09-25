
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class BOMItemRepository : IBOMItemRepository
{
    private readonly ApplicationDbContext _db;

    public BOMItemRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<BOMItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.BOMItems
            .Include(x => x.Bom)
            .Include(x => x.Component)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<BOMItem>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.BOMItems
            .AsNoTracking()
            .Include(x => x.Bom)
            .Include(x => x.Component)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(BOMItem bOMItem, CancellationToken cancellationToken)
    {
        _db.BOMItems.Add(bOMItem);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(BOMItem bOMItem, CancellationToken cancellationToken)
    {
        _db.BOMItems.Update(bOMItem);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(BOMItem bOMItem, CancellationToken cancellationToken)
    {
        _db.BOMItems.Remove(bOMItem);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
