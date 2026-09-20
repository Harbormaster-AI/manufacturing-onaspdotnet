using manufacturingonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class ProductionLineRepository : IProductionLineRepository
{
    private readonly ApplicationDbContext _db;

    public ProductionLineRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ProductionLine?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ProductionLines
            .Include(x => x.Plant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ProductionLine>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ProductionLines
            .AsNoTracking()
            .Include(x => x.Plant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ProductionLine productionLine, CancellationToken cancellationToken)
    {
        _db.ProductionLines.Add(productionLine);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ProductionLine productionLine, CancellationToken cancellationToken)
    {
        _db.ProductionLines.Update(productionLine);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ProductionLine productionLine, CancellationToken cancellationToken)
    {
        _db.ProductionLines.Remove(productionLine);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
