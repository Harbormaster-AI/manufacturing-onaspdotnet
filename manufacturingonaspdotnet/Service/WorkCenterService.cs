
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Telemetry;

namespace manufacturingonaspdotnet.Service;

public interface IWorkCenterService {

    Task Create(WorkCenter model , CancellationToken cancellationToken);
    Task<bool> Update(WorkCenter model, CancellationToken cancellationToken);
    Task<WorkCenter?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<WorkCenter>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignProductionLine(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignProductionLine(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToAssets(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAssets(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToMaintenanceOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromMaintenanceOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class WorkCenterService : IWorkCenterService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IWorkCenterRepository _repository;
    private readonly ILogger<WorkCenterService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public WorkCenterService(
        ApplicationTelemetry telemetry,
        IWorkCenterRepository repository,
        ILogger<WorkCenterService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(WorkCenter model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "WorkCenter",
                "CreateWorkCenter",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(WorkCenter model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Code = model.Code;
            existing.CapacityPerHour = model.CapacityPerHour;
            existing.OeeTarget = model.OeeTarget;
            existing.WorkCenterType = model.WorkCenterType;

            await _telemetry.Execute(
                "WorkCenter",
                "UpdateWorkCenter",
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

    public Task<WorkCenter?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<WorkCenter>> GetAll(CancellationToken cancellationToken)
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
                "WorkCenter",
                "UpdateWorkCenter",
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

    public async Task<bool> AssignProductionLine(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No WorkCenter found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<ProductionLineService>().Get(childRequest, cancellationToken);
            parent.ProductionLine = child;
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

    public async Task<bool> UnassignProductionLine(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No WorkCenter found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.ProductionLine = null;
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


    public async Task<bool> AddToAssets(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "WorkCenter",
                "AddToAssets",
                () => _repository.AddToAssetsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromAssets(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "WorkCenter",
                "RemoveFromAssets",
                () => _repository.RemoveFromAssetsAsync(request, cancellationToken));
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

    public async Task<bool> AddToMaintenanceOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "WorkCenter",
                "AddToMaintenanceOrders",
                () => _repository.AddToMaintenanceOrdersAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromMaintenanceOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "WorkCenter",
                "RemoveFromMaintenanceOrders",
                () => _repository.RemoveFromMaintenanceOrdersAsync(request, cancellationToken));
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
