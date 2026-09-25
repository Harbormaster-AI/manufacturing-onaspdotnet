
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class InventoryItemRepository : IInventoryItemRepository
{
    private readonly ApplicationDbContext _db;

    public InventoryItemRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<InventoryItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.InventoryItems
            .Include(x => x.Item)
            .Include(x => x.Location)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<InventoryItem>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.InventoryItems
            .AsNoTracking()
            .Include(x => x.Item)
            .Include(x => x.Location)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(InventoryItem inventoryItem, CancellationToken cancellationToken)
    {
        _db.InventoryItems.Add(inventoryItem);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(InventoryItem inventoryItem, CancellationToken cancellationToken)
    {
        _db.InventoryItems.Update(inventoryItem);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(InventoryItem inventoryItem, CancellationToken cancellationToken)
    {
        _db.InventoryItems.Remove(inventoryItem);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
