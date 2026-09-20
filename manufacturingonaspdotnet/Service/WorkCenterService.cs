using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;

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
    private readonly IWorkCenterRepository _repository;
    private readonly ILogger<WorkCenterService> _logger;

    public WorkCenterService(
        IWorkCenterRepository repository, ILogger<WorkCenterService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(WorkCenter model, CancellationToken cancellationToken)
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

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
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
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }

    public async Task<bool> AssignProductionLine(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignProductionLine(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToAssets(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromAssets(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToMaintenanceOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromMaintenanceOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
