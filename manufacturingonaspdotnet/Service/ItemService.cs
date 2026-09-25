
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Telemetry;

namespace manufacturingonaspdotnet.Service;

public interface IItemService {

    Task Create(Item model , CancellationToken cancellationToken);
    Task<bool> Update(Item model, CancellationToken cancellationToken);
    Task<Item?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Item>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignBusinessUnit(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignBusinessUnit(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToBoms(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromBoms(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToRoutings(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromRoutings(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToSuppliers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromSuppliers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToQualitySpecifications(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromQualitySpecifications(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToInventoryItems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromInventoryItems(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class ItemService : IItemService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IItemRepository _repository;
    private readonly ILogger<ItemService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public ItemService(
        ApplicationTelemetry telemetry,
        IItemRepository repository,
        ILogger<ItemService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Item model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Item",
                "CreateItem",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Item model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.ItemNumber = model.ItemNumber;
            existing.Name = model.Name;
            existing.StandardCost = model.StandardCost;
            existing.Weight = model.Weight;
            existing.AsSerialControlled = model.AsSerialControlled;
            existing.ItemType = model.ItemType;
            existing.ProcurementType = model.ProcurementType;
            existing.UnitOfMeasure = model.UnitOfMeasure;
            existing.LifecycleStatus = model.LifecycleStatus;

            await _telemetry.Execute(
                "Item",
                "UpdateItem",
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

    public Task<Item?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Item>> GetAll(CancellationToken cancellationToken)
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
                "Item",
                "UpdateItem",
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

    public async Task<bool> AssignBusinessUnit(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Item found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<BusinessUnitService>().Get(childRequest, cancellationToken);
            parent.BusinessUnit = child;
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

    public async Task<bool> UnassignBusinessUnit(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Item found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.BusinessUnit = null;
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


    public async Task<bool> AddToBoms(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Item",
                "AddToBoms",
                () => _repository.AddToBomsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromBoms(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Item",
                "RemoveFromBoms",
                () => _repository.RemoveFromBomsAsync(request, cancellationToken));
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

    public async Task<bool> AddToRoutings(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Item",
                "AddToRoutings",
                () => _repository.AddToRoutingsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromRoutings(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Item",
                "RemoveFromRoutings",
                () => _repository.RemoveFromRoutingsAsync(request, cancellationToken));
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

    public async Task<bool> AddToSuppliers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Item",
                "AddToSuppliers",
                () => _repository.AddToSuppliersAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromSuppliers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Item",
                "RemoveFromSuppliers",
                () => _repository.RemoveFromSuppliersAsync(request, cancellationToken));
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

    public async Task<bool> AddToQualitySpecifications(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Item",
                "AddToQualitySpecifications",
                () => _repository.AddToQualitySpecificationsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromQualitySpecifications(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Item",
                "RemoveFromQualitySpecifications",
                () => _repository.RemoveFromQualitySpecificationsAsync(request, cancellationToken));
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

    public async Task<bool> AddToInventoryItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Item",
                "AddToInventoryItems",
                () => _repository.AddToInventoryItemsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromInventoryItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Item",
                "RemoveFromInventoryItems",
                () => _repository.RemoveFromInventoryItemsAsync(request, cancellationToken));
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
