
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class SalesOrderLineRepository : ISalesOrderLineRepository
{
    private readonly ApplicationDbContext _db;

    public SalesOrderLineRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<SalesOrderLine?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.SalesOrderLines
            .Include(x => x.SalesOrder)
            .Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<SalesOrderLine>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.SalesOrderLines
            .AsNoTracking()
            .Include(x => x.SalesOrder)
            .Include(x => x.Item)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(SalesOrderLine salesOrderLine, CancellationToken cancellationToken)
    {
        _db.SalesOrderLines.Add(salesOrderLine);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(SalesOrderLine salesOrderLine, CancellationToken cancellationToken)
    {
        _db.SalesOrderLines.Update(salesOrderLine);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(SalesOrderLine salesOrderLine, CancellationToken cancellationToken)
    {
        _db.SalesOrderLines.Remove(salesOrderLine);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
