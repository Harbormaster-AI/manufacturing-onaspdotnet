using manufacturingonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class SalesOrderRepository : ISalesOrderRepository
{
    private readonly ApplicationDbContext _db;

    public SalesOrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<SalesOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.SalesOrders
            .Include(x => x.Customer)
            .Include(x => x.Plant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<SalesOrder>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.SalesOrders
            .AsNoTracking()
            .Include(x => x.Customer)
            .Include(x => x.Plant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(SalesOrder salesOrder, CancellationToken cancellationToken)
    {
        _db.SalesOrders.Add(salesOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(SalesOrder salesOrder, CancellationToken cancellationToken)
    {
        _db.SalesOrders.Update(salesOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(SalesOrder salesOrder, CancellationToken cancellationToken)
    {
        _db.SalesOrders.Remove(salesOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
