using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IPlantRepository
{
    Task<Plant?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Plant>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Plant plant, CancellationToken cancellationToken);
    Task UpdateAsync(Plant plant, CancellationToken cancellationToken);
    Task DeleteAsync(Plant plant, CancellationToken cancellationToken);

    Task AddToProductionLinesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromProductionLinesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToWorkCentersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromWorkCentersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToWarehousesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromWarehousesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToAssetsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAssetsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToProductionSchedulesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromProductionSchedulesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
