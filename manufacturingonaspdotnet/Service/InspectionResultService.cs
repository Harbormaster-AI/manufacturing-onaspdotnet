using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Service;

public interface IInspectionResultService {

    Task Create(InspectionResult model , CancellationToken cancellationToken);
    Task<bool> Update(InspectionResult model, CancellationToken cancellationToken);
    Task<InspectionResult?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<InspectionResult>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignInspectionLot(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignInspectionLot(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignCharacteristic(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCharacteristic(AssociationRequest request, CancellationToken cancellationToken);


}

public class InspectionResultService : IInspectionResultService
{
    private readonly IInspectionResultRepository _repository;
    private readonly ILogger<InspectionResultService> _logger;

    public InspectionResultService(
        IInspectionResultRepository repository, ILogger<InspectionResultService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(InspectionResult model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(InspectionResult model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.ResultValue = model.ResultValue;
            existing.RecordedOn = model.RecordedOn;
            existing.Notes = model.Notes;
            existing.ResultStatus = model.ResultStatus;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<InspectionResult?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<InspectionResult>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignInspectionLot(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignInspectionLot(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignCharacteristic(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCharacteristic(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
