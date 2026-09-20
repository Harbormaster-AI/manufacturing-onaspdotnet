using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Service;

public interface IMaintenanceOrderService {

    Task Create(MaintenanceOrder model , CancellationToken cancellationToken);
    Task<bool> Update(MaintenanceOrder model, CancellationToken cancellationToken);
    Task<MaintenanceOrder?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<MaintenanceOrder>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignAsset(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAsset(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignPlan(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPlan(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignWorkCenter(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignWorkCenter(AssociationRequest request, CancellationToken cancellationToken);


}

public class MaintenanceOrderService : IMaintenanceOrderService
{
    private readonly IMaintenanceOrderRepository _repository;
    private readonly ILogger<MaintenanceOrderService> _logger;

    public MaintenanceOrderService(
        IMaintenanceOrderRepository repository, ILogger<MaintenanceOrderService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(MaintenanceOrder model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(MaintenanceOrder model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.OrderNumber = model.OrderNumber;
            existing.Priority = model.Priority;
            existing.RequestedDate = model.RequestedDate;
            existing.CompletionDate = model.CompletionDate;
            existing.Status = model.Status;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<MaintenanceOrder?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<MaintenanceOrder>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignAsset(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignAsset(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignPlan(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPlan(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignWorkCenter(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignWorkCenter(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
