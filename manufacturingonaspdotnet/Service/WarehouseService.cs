using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Service;

public interface IWarehouseService {

    Task Create(Warehouse model , CancellationToken cancellationToken);
    Task<bool> Update(Warehouse model, CancellationToken cancellationToken);
    Task<Warehouse?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Warehouse>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignPlant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPlant(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToLocations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromLocations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToInventoryItems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromInventoryItems(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _repository;
    private readonly ILogger<WarehouseService> _logger;

    public WarehouseService(
        IWarehouseRepository repository, ILogger<WarehouseService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Warehouse model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Warehouse model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.WarehouseCode = model.WarehouseCode;
            existing.Address = model.Address;
            existing.WarehouseType = model.WarehouseType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Warehouse?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Warehouse>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignPlant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPlant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToLocations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromLocations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToInventoryItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromInventoryItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
