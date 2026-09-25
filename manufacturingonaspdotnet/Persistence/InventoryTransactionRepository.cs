
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class InventoryTransactionRepository : IInventoryTransactionRepository
{
    private readonly ApplicationDbContext _db;

    public InventoryTransactionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<InventoryTransaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.InventoryTransactions
            .Include(x => x.Item)
            .Include(x => x.Location)
            .Include(x => x.WorkOrder)
            .Include(x => x.PurchaseOrder)
            .Include(x => x.SalesOrder)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<InventoryTransaction>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.InventoryTransactions
            .AsNoTracking()
            .Include(x => x.Item)
            .Include(x => x.Location)
            .Include(x => x.WorkOrder)
            .Include(x => x.PurchaseOrder)
            .Include(x => x.SalesOrder)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(InventoryTransaction inventoryTransaction, CancellationToken cancellationToken)
    {
        _db.InventoryTransactions.Add(inventoryTransaction);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(InventoryTransaction inventoryTransaction, CancellationToken cancellationToken)
    {
        _db.InventoryTransactions.Update(inventoryTransaction);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(InventoryTransaction inventoryTransaction, CancellationToken cancellationToken)
    {
        _db.InventoryTransactions.Remove(inventoryTransaction);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
