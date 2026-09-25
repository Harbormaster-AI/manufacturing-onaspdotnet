using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IQualitySpecificationRepository
{
    Task<QualitySpecification?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<QualitySpecification>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(QualitySpecification qualitySpecification, CancellationToken cancellationToken);
    Task UpdateAsync(QualitySpecification qualitySpecification, CancellationToken cancellationToken);
    Task DeleteAsync(QualitySpecification qualitySpecification, CancellationToken cancellationToken);


}
