
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Telemetry;

namespace manufacturingonaspdotnet.Service;

public interface IInspectionPlanService {

    Task Create(InspectionPlan model , CancellationToken cancellationToken);
    Task<bool> Update(InspectionPlan model, CancellationToken cancellationToken);
    Task<InspectionPlan?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<InspectionPlan>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignItem(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignItem(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToCharacteristics(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCharacteristics(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class InspectionPlanService : IInspectionPlanService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IInspectionPlanRepository _repository;
    private readonly ILogger<InspectionPlanService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public InspectionPlanService(
        ApplicationTelemetry telemetry,
        IInspectionPlanRepository repository,
        ILogger<InspectionPlanService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(InspectionPlan model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "InspectionPlan",
                "CreateInspectionPlan",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(InspectionPlan model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.PlanNumber = model.PlanNumber;
            existing.Revision = model.Revision;
            existing.SamplingPlan = model.SamplingPlan;
            existing.Status = model.Status;

            await _telemetry.Execute(
                "InspectionPlan",
                "UpdateInspectionPlan",
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

    public Task<InspectionPlan?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<InspectionPlan>> GetAll(CancellationToken cancellationToken)
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
                "InspectionPlan",
                "UpdateInspectionPlan",
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

    public async Task<bool> AssignItem(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No InspectionPlan found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<ItemService>().Get(childRequest, cancellationToken);
            parent.Item = child;
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

    public async Task<bool> UnassignItem(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No InspectionPlan found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Item = null;
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


    public async Task<bool> AddToCharacteristics(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "InspectionPlan",
                "AddToCharacteristics",
                () => _repository.AddToCharacteristicsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromCharacteristics(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "InspectionPlan",
                "RemoveFromCharacteristics",
                () => _repository.RemoveFromCharacteristicsAsync(request, cancellationToken));
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
