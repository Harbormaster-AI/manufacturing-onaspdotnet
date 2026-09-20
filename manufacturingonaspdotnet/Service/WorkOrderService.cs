using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Service;

public interface IWorkOrderService {

    Task Create(WorkOrder model , CancellationToken cancellationToken);
    Task<bool> Update(WorkOrder model, CancellationToken cancellationToken);
    Task<WorkOrder?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<WorkOrder>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignItem(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignItem(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignPlant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPlant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignRouting(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRouting(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignBom(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignBom(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignProductionSchedule(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignProductionSchedule(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignSalesOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignSalesOrder(AssociationRequest request, CancellationToken cancellationToken);


}

public class WorkOrderService : IWorkOrderService
{
    private readonly IWorkOrderRepository _repository;
    private readonly ILogger<WorkOrderService> _logger;

    public WorkOrderService(
        IWorkOrderRepository repository, ILogger<WorkOrderService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(WorkOrder model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(WorkOrder model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.WorkOrderNumber = model.WorkOrderNumber;
            existing.PlannedStart = model.PlannedStart;
            existing.PlannedEnd = model.PlannedEnd;
            existing.Quantity = model.Quantity;
            existing.Priority = model.Priority;
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

    public Task<WorkOrder?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<WorkOrder>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignItem(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignItem(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignPlant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPlant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignRouting(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignRouting(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignBom(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignBom(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignProductionSchedule(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignProductionSchedule(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignSalesOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignSalesOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
