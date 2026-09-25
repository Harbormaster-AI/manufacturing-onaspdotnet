
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class QualitySpecificationRepository : IQualitySpecificationRepository
{
    private readonly ApplicationDbContext _db;

    public QualitySpecificationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<QualitySpecification?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.QualitySpecifications
            .Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<QualitySpecification>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.QualitySpecifications
            .AsNoTracking()
            .Include(x => x.Item)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(QualitySpecification qualitySpecification, CancellationToken cancellationToken)
    {
        _db.QualitySpecifications.Add(qualitySpecification);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(QualitySpecification qualitySpecification, CancellationToken cancellationToken)
    {
        _db.QualitySpecifications.Update(qualitySpecification);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(QualitySpecification qualitySpecification, CancellationToken cancellationToken)
    {
        _db.QualitySpecifications.Remove(qualitySpecification);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
