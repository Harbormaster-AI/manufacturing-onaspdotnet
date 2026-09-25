
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Telemetry;

namespace manufacturingonaspdotnet.Service;

public interface IBusinessUnitService {

    Task Create(BusinessUnit model , CancellationToken cancellationToken);
    Task<bool> Update(BusinessUnit model, CancellationToken cancellationToken);
    Task<BusinessUnit?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<BusinessUnit>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignEnterprise(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignEnterprise(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToItems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromItems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPlants(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPlants(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class BusinessUnitService : IBusinessUnitService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IBusinessUnitRepository _repository;
    private readonly ILogger<BusinessUnitService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public BusinessUnitService(
        ApplicationTelemetry telemetry,
        IBusinessUnitRepository repository,
        ILogger<BusinessUnitService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(BusinessUnit model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "BusinessUnit",
                "CreateBusinessUnit",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(BusinessUnit model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Code = model.Code;
            existing.Category = model.Category;

            await _telemetry.Execute(
                "BusinessUnit",
                "UpdateBusinessUnit",
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

    public Task<BusinessUnit?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<BusinessUnit>> GetAll(CancellationToken cancellationToken)
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
                "BusinessUnit",
                "UpdateBusinessUnit",
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
            _logger.LogError("No BusinessUnit found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No BusinessUnit found using Id {ParentId}", request.ParentId);
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


    public async Task<bool> AddToItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "BusinessUnit",
                "AddToItems",
                () => _repository.AddToItemsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "BusinessUnit",
                "RemoveFromItems",
                () => _repository.RemoveFromItemsAsync(request, cancellationToken));
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

    public async Task<bool> AddToPlants(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "BusinessUnit",
                "AddToPlants",
                () => _repository.AddToPlantsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromPlants(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "BusinessUnit",
                "RemoveFromPlants",
                () => _repository.RemoveFromPlantsAsync(request, cancellationToken));
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
