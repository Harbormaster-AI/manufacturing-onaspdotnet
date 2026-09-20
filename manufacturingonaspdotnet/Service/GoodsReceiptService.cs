using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Service;

public interface IGoodsReceiptService {

    Task Create(GoodsReceipt model , CancellationToken cancellationToken);
    Task<bool> Update(GoodsReceipt model, CancellationToken cancellationToken);
    Task<GoodsReceipt?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<GoodsReceipt>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignPurchaseOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPurchaseOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignWarehouse(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignWarehouse(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToLines(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromLines(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class GoodsReceiptService : IGoodsReceiptService
{
    private readonly IGoodsReceiptRepository _repository;
    private readonly ILogger<GoodsReceiptService> _logger;

    public GoodsReceiptService(
        IGoodsReceiptRepository repository, ILogger<GoodsReceiptService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(GoodsReceipt model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(GoodsReceipt model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.ReceiptNumber = model.ReceiptNumber;
            existing.ReceiptDate = model.ReceiptDate;
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

    public Task<GoodsReceipt?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<GoodsReceipt>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignPurchaseOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPurchaseOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignWarehouse(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignWarehouse(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToLines(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromLines(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
