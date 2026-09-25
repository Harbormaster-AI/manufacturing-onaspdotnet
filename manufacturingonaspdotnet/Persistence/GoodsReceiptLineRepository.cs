
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class GoodsReceiptLineRepository : IGoodsReceiptLineRepository
{
    private readonly ApplicationDbContext _db;

    public GoodsReceiptLineRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<GoodsReceiptLine?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.GoodsReceiptLines
            .Include(x => x.GoodsReceipt)
            .Include(x => x.Item)
            .Include(x => x.InventoryTransaction)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<GoodsReceiptLine>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.GoodsReceiptLines
            .AsNoTracking()
            .Include(x => x.GoodsReceipt)
            .Include(x => x.Item)
            .Include(x => x.InventoryTransaction)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(GoodsReceiptLine goodsReceiptLine, CancellationToken cancellationToken)
    {
        _db.GoodsReceiptLines.Add(goodsReceiptLine);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(GoodsReceiptLine goodsReceiptLine, CancellationToken cancellationToken)
    {
        _db.GoodsReceiptLines.Update(goodsReceiptLine);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(GoodsReceiptLine goodsReceiptLine, CancellationToken cancellationToken)
    {
        _db.GoodsReceiptLines.Remove(goodsReceiptLine);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
