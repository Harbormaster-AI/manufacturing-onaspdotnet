using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IInspectionResultRepository
{
    Task<InspectionResult?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<InspectionResult>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(InspectionResult inspectionResult, CancellationToken cancellationToken);
    Task UpdateAsync(InspectionResult inspectionResult, CancellationToken cancellationToken);
    Task DeleteAsync(InspectionResult inspectionResult, CancellationToken cancellationToken);


}
