using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Service;

public interface IInspectionCharacteristicService {

    Task Create(InspectionCharacteristic model , CancellationToken cancellationToken);
    Task<bool> Update(InspectionCharacteristic model, CancellationToken cancellationToken);
    Task<InspectionCharacteristic?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<InspectionCharacteristic>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignInspectionPlan(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignInspectionPlan(AssociationRequest request, CancellationToken cancellationToken);


}

public class InspectionCharacteristicService : IInspectionCharacteristicService
{
    private readonly IInspectionCharacteristicRepository _repository;
    private readonly ILogger<InspectionCharacteristicService> _logger;

    public InspectionCharacteristicService(
        IInspectionCharacteristicRepository repository, ILogger<InspectionCharacteristicService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(InspectionCharacteristic model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(InspectionCharacteristic model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.CharacteristicCode = model.CharacteristicCode;
            existing.Name = model.Name;
            existing.LowerSpecLimit = model.LowerSpecLimit;
            existing.UpperSpecLimit = model.UpperSpecLimit;
            existing.Target = model.Target;
            existing.MeasurementType = model.MeasurementType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<InspectionCharacteristic?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<InspectionCharacteristic>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignInspectionPlan(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignInspectionPlan(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
