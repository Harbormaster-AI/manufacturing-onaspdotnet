
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class PurchaseOrderLineRepository : IPurchaseOrderLineRepository
{
    private readonly ApplicationDbContext _db;

    public PurchaseOrderLineRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PurchaseOrderLine?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.PurchaseOrderLines
            .Include(x => x.PurchaseOrder)
            .Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PurchaseOrderLine>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.PurchaseOrderLines
            .AsNoTracking()
            .Include(x => x.PurchaseOrder)
            .Include(x => x.Item)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PurchaseOrderLine purchaseOrderLine, CancellationToken cancellationToken)
    {
        _db.PurchaseOrderLines.Add(purchaseOrderLine);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PurchaseOrderLine purchaseOrderLine, CancellationToken cancellationToken)
    {
        _db.PurchaseOrderLines.Update(purchaseOrderLine);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PurchaseOrderLine purchaseOrderLine, CancellationToken cancellationToken)
    {
        _db.PurchaseOrderLines.Remove(purchaseOrderLine);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
