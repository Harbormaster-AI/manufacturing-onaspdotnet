using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Service;

public interface IInventoryTransactionService {

    Task Create(InventoryTransaction model , CancellationToken cancellationToken);
    Task<bool> Update(InventoryTransaction model, CancellationToken cancellationToken);
    Task<InventoryTransaction?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<InventoryTransaction>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignItem(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignItem(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignLocation(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignLocation(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignWorkOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignWorkOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignPurchaseOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPurchaseOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignSalesOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignSalesOrder(AssociationRequest request, CancellationToken cancellationToken);


}

public class InventoryTransactionService : IInventoryTransactionService
{
    private readonly IInventoryTransactionRepository _repository;
    private readonly ILogger<InventoryTransactionService> _logger;

    public InventoryTransactionService(
        IInventoryTransactionRepository repository, ILogger<InventoryTransactionService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(InventoryTransaction model, CancellationToken cancellationToken)
    {

 
 
 
 
         try
        {
            await _repository.AddAsync(model, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
        }
    }

    public async Task<bool> Update(InventoryTransaction model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.TransactionNumber = model.TransactionNumber;
            existing.Quantity = model.Quantity;
            existing.TransactionDateTime = model.TransactionDateTime;
            existing.ReferenceDocument = model.ReferenceDocument;
            existing.TransactionType = model.TransactionType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<InventoryTransaction?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<InventoryTransaction>> GetAll(CancellationToken cancellationToken)
    => _repository.GetAllAsync(cancellationToken);

    public async Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        try
        {
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }

    public async Task<bool> AssignItem(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignItem(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignLocation(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignLocation(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignWorkOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignWorkOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignPurchaseOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPurchaseOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignSalesOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignSalesOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
