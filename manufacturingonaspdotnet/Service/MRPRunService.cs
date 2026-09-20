using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Service;

public interface IMRPRunService {

    Task Create(MRPRun model , CancellationToken cancellationToken);
    Task<bool> Update(MRPRun model, CancellationToken cancellationToken);
    Task<MRPRun?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<MRPRun>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignPlant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPlant(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToPlannedOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPlannedOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class MRPRunService : IMRPRunService
{
    private readonly IMRPRunRepository _repository;
    private readonly ILogger<MRPRunService> _logger;

    public MRPRunService(
        IMRPRunRepository repository, ILogger<MRPRunService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(MRPRun model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(MRPRun model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.RunNumber = model.RunNumber;
            existing.RunDateTime = model.RunDateTime;
            existing.PlanningHorizonDays = model.PlanningHorizonDays;
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

    public Task<MRPRun?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<MRPRun>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToPlannedOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPlannedOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
