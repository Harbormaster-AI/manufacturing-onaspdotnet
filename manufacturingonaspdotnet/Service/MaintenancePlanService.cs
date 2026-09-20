using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Service;

public interface IMaintenancePlanService {

    Task Create(MaintenancePlan model , CancellationToken cancellationToken);
    Task<bool> Update(MaintenancePlan model, CancellationToken cancellationToken);
    Task<MaintenancePlan?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<MaintenancePlan>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignAsset(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAsset(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToMaintenanceOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromMaintenanceOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class MaintenancePlanService : IMaintenancePlanService
{
    private readonly IMaintenancePlanRepository _repository;
    private readonly ILogger<MaintenancePlanService> _logger;

    public MaintenancePlanService(
        IMaintenancePlanRepository repository, ILogger<MaintenancePlanService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(MaintenancePlan model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(MaintenancePlan model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.PlanNumber = model.PlanNumber;
            existing.Interval = model.Interval;
            existing.LastServiceDate = model.LastServiceDate;
            existing.Strategy = model.Strategy;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<MaintenancePlan?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<MaintenancePlan>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToMaintenanceOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromMaintenanceOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
