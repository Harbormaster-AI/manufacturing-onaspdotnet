using manufacturingonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class SupplierRepository : ISupplierRepository
{
    private readonly ApplicationDbContext _db;

    public SupplierRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Supplier?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Suppliers
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Supplier>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Suppliers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Supplier supplier, CancellationToken cancellationToken)
    {
        _db.Suppliers.Add(supplier);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Supplier supplier, CancellationToken cancellationToken)
    {
        _db.Suppliers.Update(supplier);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Supplier supplier, CancellationToken cancellationToken)
    {
        _db.Suppliers.Remove(supplier);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
