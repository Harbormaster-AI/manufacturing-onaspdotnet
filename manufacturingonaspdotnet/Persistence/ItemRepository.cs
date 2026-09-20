using manufacturingonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class ItemRepository : IItemRepository
{
    private readonly ApplicationDbContext _db;

    public ItemRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Item?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Items
            .Include(x => x.BusinessUnit)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Item>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Items
            .AsNoTracking()
            .Include(x => x.BusinessUnit)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Item item, CancellationToken cancellationToken)
    {
        _db.Items.Add(item);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Item item, CancellationToken cancellationToken)
    {
        _db.Items.Update(item);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Item item, CancellationToken cancellationToken)
    {
        _db.Items.Remove(item);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
