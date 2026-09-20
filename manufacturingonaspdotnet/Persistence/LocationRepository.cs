using manufacturingonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class LocationRepository : ILocationRepository
{
    private readonly ApplicationDbContext _db;

    public LocationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Location?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Locations
            .Include(x => x.Warehouse)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Location>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Locations
            .AsNoTracking()
            .Include(x => x.Warehouse)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Location location, CancellationToken cancellationToken)
    {
        _db.Locations.Add(location);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Location location, CancellationToken cancellationToken)
    {
        _db.Locations.Update(location);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Location location, CancellationToken cancellationToken)
    {
        _db.Locations.Remove(location);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
