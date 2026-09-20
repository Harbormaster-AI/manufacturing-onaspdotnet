using manufacturingonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class PurchaseOrderRepository : IPurchaseOrderRepository
{
    private readonly ApplicationDbContext _db;

    public PurchaseOrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PurchaseOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.PurchaseOrders
            .Include(x => x.Supplier)
            .Include(x => x.Plant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PurchaseOrder>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.PurchaseOrders
            .AsNoTracking()
            .Include(x => x.Supplier)
            .Include(x => x.Plant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PurchaseOrder purchaseOrder, CancellationToken cancellationToken)
    {
        _db.PurchaseOrders.Add(purchaseOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PurchaseOrder purchaseOrder, CancellationToken cancellationToken)
    {
        _db.PurchaseOrders.Update(purchaseOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PurchaseOrder purchaseOrder, CancellationToken cancellationToken)
    {
        _db.PurchaseOrders.Remove(purchaseOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
