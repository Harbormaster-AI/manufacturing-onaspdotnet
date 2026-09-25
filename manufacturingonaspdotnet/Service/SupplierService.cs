
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Telemetry;

namespace manufacturingonaspdotnet.Service;

public interface ISupplierService {

    Task Create(Supplier model , CancellationToken cancellationToken);
    Task<bool> Update(Supplier model, CancellationToken cancellationToken);
    Task<Supplier?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Supplier>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToEnterprises(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromEnterprises(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToItems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromItems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPurchaseOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPurchaseOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class SupplierService : ISupplierService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly ISupplierRepository _repository;
    private readonly ILogger<SupplierService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public SupplierService(
        ApplicationTelemetry telemetry,
        ISupplierRepository repository,
        ILogger<SupplierService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Supplier model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Supplier",
                "CreateSupplier",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Supplier model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.SupplierCode = model.SupplierCode;
            existing.Address = model.Address;
            existing.SupplierTier = model.SupplierTier;
            existing.PaymentTerms = model.PaymentTerms;

            await _telemetry.Execute(
                "Supplier",
                "UpdateSupplier",
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

    public Task<Supplier?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Supplier>> GetAll(CancellationToken cancellationToken)
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
                "Supplier",
                "UpdateSupplier",
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


    public async Task<bool> AddToEnterprises(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Supplier",
                "AddToEnterprises",
                () => _repository.AddToEnterprisesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromEnterprises(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Supplier",
                "RemoveFromEnterprises",
                () => _repository.RemoveFromEnterprisesAsync(request, cancellationToken));
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
                "Supplier",
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
                "Supplier",
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

    public async Task<bool> AddToPurchaseOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Supplier",
                "AddToPurchaseOrders",
                () => _repository.AddToPurchaseOrdersAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromPurchaseOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Supplier",
                "RemoveFromPurchaseOrders",
                () => _repository.RemoveFromPurchaseOrdersAsync(request, cancellationToken));
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
