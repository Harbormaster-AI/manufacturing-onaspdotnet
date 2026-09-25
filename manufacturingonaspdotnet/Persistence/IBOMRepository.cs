using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IBOMRepository
{
    Task<BOM?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<BOM>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(BOM bOM, CancellationToken cancellationToken);
    Task UpdateAsync(BOM bOM, CancellationToken cancellationToken);
    Task DeleteAsync(BOM bOM, CancellationToken cancellationToken);

    Task AddToBomItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromBomItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
