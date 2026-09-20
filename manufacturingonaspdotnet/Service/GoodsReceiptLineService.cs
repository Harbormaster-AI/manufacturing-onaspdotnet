using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Service;

public interface IGoodsReceiptLineService {

    Task Create(GoodsReceiptLine model , CancellationToken cancellationToken);
    Task<bool> Update(GoodsReceiptLine model, CancellationToken cancellationToken);
    Task<GoodsReceiptLine?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<GoodsReceiptLine>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignGoodsReceipt(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignGoodsReceipt(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignItem(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignItem(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignInventoryTransaction(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignInventoryTransaction(AssociationRequest request, CancellationToken cancellationToken);


}

public class GoodsReceiptLineService : IGoodsReceiptLineService
{
    private readonly IGoodsReceiptLineRepository _repository;
    private readonly ILogger<GoodsReceiptLineService> _logger;

    public GoodsReceiptLineService(
        IGoodsReceiptLineRepository repository, ILogger<GoodsReceiptLineService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(GoodsReceiptLine model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(GoodsReceiptLine model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.LineNumber = model.LineNumber;
            existing.ReceivedQuantity = model.ReceivedQuantity;
            existing.AcceptedQuantity = model.AcceptedQuantity;
            existing.RejectedQuantity = model.RejectedQuantity;
            existing.Lot = model.Lot;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<GoodsReceiptLine?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<GoodsReceiptLine>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignGoodsReceipt(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignGoodsReceipt(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignItem(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignItem(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignInventoryTransaction(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignInventoryTransaction(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
