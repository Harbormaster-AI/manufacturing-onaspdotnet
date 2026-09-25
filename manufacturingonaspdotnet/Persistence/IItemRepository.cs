using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IItemRepository
{
    Task<Item?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Item>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Item item, CancellationToken cancellationToken);
    Task UpdateAsync(Item item, CancellationToken cancellationToken);
    Task DeleteAsync(Item item, CancellationToken cancellationToken);

    Task AddToBomsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromBomsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToRoutingsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromRoutingsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToSuppliersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromSuppliersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToQualitySpecificationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromQualitySpecificationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToInventoryItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromInventoryItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
