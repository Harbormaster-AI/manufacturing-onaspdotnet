using manufacturingonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly ApplicationDbContext _db;

    public WarehouseRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Warehouse?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Warehouses
            .Include(x => x.Plant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Warehouse>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Warehouses
            .AsNoTracking()
            .Include(x => x.Plant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Warehouse warehouse, CancellationToken cancellationToken)
    {
        _db.Warehouses.Add(warehouse);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Warehouse warehouse, CancellationToken cancellationToken)
    {
        _db.Warehouses.Update(warehouse);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Warehouse warehouse, CancellationToken cancellationToken)
    {
        _db.Warehouses.Remove(warehouse);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
