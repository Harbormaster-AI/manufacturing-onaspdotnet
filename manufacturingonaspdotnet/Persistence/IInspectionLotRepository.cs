using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IInspectionLotRepository
{
    Task<InspectionLot?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<InspectionLot>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(InspectionLot inspectionLot, CancellationToken cancellationToken);
    Task UpdateAsync(InspectionLot inspectionLot, CancellationToken cancellationToken);
    Task DeleteAsync(InspectionLot inspectionLot, CancellationToken cancellationToken);

    Task AddToResultsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromResultsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
