using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Service;

public interface INonconformanceService {

    Task Create(Nonconformance model , CancellationToken cancellationToken);
    Task<bool> Update(Nonconformance model, CancellationToken cancellationToken);
    Task<Nonconformance?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Nonconformance>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignItem(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignItem(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignWorkOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignWorkOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignInspectionLot(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignInspectionLot(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignCorrectiveAction(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCorrectiveAction(AssociationRequest request, CancellationToken cancellationToken);


}

public class NonconformanceService : INonconformanceService
{
    private readonly INonconformanceRepository _repository;
    private readonly ILogger<NonconformanceService> _logger;

    public NonconformanceService(
        INonconformanceRepository repository, ILogger<NonconformanceService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Nonconformance model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Nonconformance model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.NcNumber = model.NcNumber;
            existing.Description = model.Description;
            existing.ContainmentAction = model.ContainmentAction;
            existing.NcType = model.NcType;
            existing.Severity = model.Severity;
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

    public Task<Nonconformance?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Nonconformance>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignWorkOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignWorkOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignInspectionLot(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignInspectionLot(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignCorrectiveAction(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCorrectiveAction(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
