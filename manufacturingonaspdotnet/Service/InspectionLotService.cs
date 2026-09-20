using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Service;

public interface IInspectionLotService {

    Task Create(InspectionLot model , CancellationToken cancellationToken);
    Task<bool> Update(InspectionLot model, CancellationToken cancellationToken);
    Task<InspectionLot?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<InspectionLot>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignItem(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignItem(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignWorkOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignWorkOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignGoodsReceipt(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignGoodsReceipt(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToResults(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromResults(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class InspectionLotService : IInspectionLotService
{
    private readonly IInspectionLotRepository _repository;
    private readonly ILogger<InspectionLotService> _logger;

    public InspectionLotService(
        IInspectionLotRepository repository, ILogger<InspectionLotService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(InspectionLot model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(InspectionLot model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.LotNumber = model.LotNumber;
            existing.Quantity = model.Quantity;
            existing.SampleSize = model.SampleSize;
            existing.CreatedOn = model.CreatedOn;
            existing.InspectionType = model.InspectionType;
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

    public Task<InspectionLot?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<InspectionLot>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignWorkOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignWorkOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignGoodsReceipt(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignGoodsReceipt(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToResults(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromResults(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
