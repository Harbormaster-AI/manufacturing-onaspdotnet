
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Telemetry;

namespace manufacturingonaspdotnet.Service;

public interface IPlantService {

    Task Create(Plant model , CancellationToken cancellationToken);
    Task<bool> Update(Plant model, CancellationToken cancellationToken);
    Task<Plant?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Plant>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignEnterprise(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignEnterprise(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToProductionLines(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromProductionLines(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToWorkCenters(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromWorkCenters(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToWarehouses(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromWarehouses(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToAssets(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAssets(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToProductionSchedules(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromProductionSchedules(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class PlantService : IPlantService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IPlantRepository _repository;
    private readonly ILogger<PlantService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public PlantService(
        ApplicationTelemetry telemetry,
        IPlantRepository repository,
        ILogger<PlantService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Plant model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Plant",
                "CreatePlant",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Plant model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.PlantCode = model.PlantCode;
            existing.Address = model.Address;
            existing.TimeZone = model.TimeZone;

            await _telemetry.Execute(
                "Plant",
                "UpdatePlant",
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

    public Task<Plant?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Plant>> GetAll(CancellationToken cancellationToken)
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
                "Plant",
                "UpdatePlant",
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

    public async Task<bool> AssignEnterprise(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Plant found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<EnterpriseService>().Get(childRequest, cancellationToken);
            parent.Enterprise = child;
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

    public async Task<bool> UnassignEnterprise(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Plant found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Enterprise = null;
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


    public async Task<bool> AddToProductionLines(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Plant",
                "AddToProductionLines",
                () => _repository.AddToProductionLinesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromProductionLines(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Plant",
                "RemoveFromProductionLines",
                () => _repository.RemoveFromProductionLinesAsync(request, cancellationToken));
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

    public async Task<bool> AddToWorkCenters(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Plant",
                "AddToWorkCenters",
                () => _repository.AddToWorkCentersAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromWorkCenters(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Plant",
                "RemoveFromWorkCenters",
                () => _repository.RemoveFromWorkCentersAsync(request, cancellationToken));
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

    public async Task<bool> AddToWarehouses(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Plant",
                "AddToWarehouses",
                () => _repository.AddToWarehousesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromWarehouses(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Plant",
                "RemoveFromWarehouses",
                () => _repository.RemoveFromWarehousesAsync(request, cancellationToken));
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
                "Plant",
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
                "Plant",
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

    public async Task<bool> AddToProductionSchedules(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Plant",
                "AddToProductionSchedules",
                () => _repository.AddToProductionSchedulesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromProductionSchedules(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Plant",
                "RemoveFromProductionSchedules",
                () => _repository.RemoveFromProductionSchedulesAsync(request, cancellationToken));
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
