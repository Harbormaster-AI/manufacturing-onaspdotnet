using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Service;

public interface IPurchaseOrderService {

    Task Create(PurchaseOrder model , CancellationToken cancellationToken);
    Task<bool> Update(PurchaseOrder model, CancellationToken cancellationToken);
    Task<PurchaseOrder?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<PurchaseOrder>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignSupplier(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignSupplier(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignPlant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPlant(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToLines(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromLines(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToGoodsReceipts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromGoodsReceipts(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class PurchaseOrderService : IPurchaseOrderService
{
    private readonly IPurchaseOrderRepository _repository;
    private readonly ILogger<PurchaseOrderService> _logger;

    public PurchaseOrderService(
        IPurchaseOrderRepository repository, ILogger<PurchaseOrderService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(PurchaseOrder model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(PurchaseOrder model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.PoNumber = model.PoNumber;
            existing.OrderDate = model.OrderDate;
            existing.TotalAmount = model.TotalAmount;
            existing.Status = model.Status;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<PurchaseOrder?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<PurchaseOrder>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignSupplier(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignSupplier(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignPlant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPlant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToLines(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromLines(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToGoodsReceipts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromGoodsReceipts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
