using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IProductionLineRepository
{
    Task<ProductionLine?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ProductionLine>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ProductionLine productionLine, CancellationToken cancellationToken);
    Task UpdateAsync(ProductionLine productionLine, CancellationToken cancellationToken);
    Task DeleteAsync(ProductionLine productionLine, CancellationToken cancellationToken);

    Task AddToWorkCentersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromWorkCentersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
