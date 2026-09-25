
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class OperationRepository : IOperationRepository
{
    private readonly ApplicationDbContext _db;

    public OperationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Operation?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Operations
            .Include(x => x.Routing)
            .Include(x => x.WorkCenter)
            .Include(x => x.InspectionPlan)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Operation>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Operations
            .AsNoTracking()
            .Include(x => x.Routing)
            .Include(x => x.WorkCenter)
            .Include(x => x.InspectionPlan)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Operation operation, CancellationToken cancellationToken)
    {
        _db.Operations.Add(operation);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Operation operation, CancellationToken cancellationToken)
    {
        _db.Operations.Update(operation);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Operation operation, CancellationToken cancellationToken)
    {
        _db.Operations.Remove(operation);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
