
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Telemetry;

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
    private readonly ApplicationTelemetry _telemetry;
    private readonly IInspectionCharacteristicRepository _repository;
    private readonly ILogger<InspectionCharacteristicService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public InspectionCharacteristicService(
        ApplicationTelemetry telemetry,
        IInspectionCharacteristicRepository repository,
        ILogger<InspectionCharacteristicService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(InspectionCharacteristic model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "InspectionCharacteristic",
                "CreateInspectionCharacteristic",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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

            await _telemetry.Execute(
                "InspectionCharacteristic",
                "UpdateInspectionCharacteristic",
                () => _repository.UpdateAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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
            await _telemetry.Execute(
                "InspectionCharacteristic",
                "UpdateInspectionCharacteristic",
                () => _repository.DeleteAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AssignInspectionPlan(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No InspectionCharacteristic found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<InspectionPlanService>().Get(childRequest, cancellationToken);
            parent.InspectionPlan = child;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> UnassignInspectionPlan(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No InspectionCharacteristic found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.InspectionPlan = null;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }




}
