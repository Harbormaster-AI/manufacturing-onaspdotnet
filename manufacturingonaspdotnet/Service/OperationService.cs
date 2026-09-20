using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Service;

public interface IOperationService {

    Task Create(Operation model , CancellationToken cancellationToken);
    Task<bool> Update(Operation model, CancellationToken cancellationToken);
    Task<Operation?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Operation>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignRouting(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRouting(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignWorkCenter(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignWorkCenter(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignInspectionPlan(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignInspectionPlan(AssociationRequest request, CancellationToken cancellationToken);


}

public class OperationService : IOperationService
{
    private readonly IOperationRepository _repository;
    private readonly ILogger<OperationService> _logger;

    public OperationService(
        IOperationRepository repository, ILogger<OperationService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Operation model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Operation model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.OperationNumber = model.OperationNumber;
            existing.Name = model.Name;
            existing.SetupTime = model.SetupTime;
            existing.StandardCycleTime = model.StandardCycleTime;
            existing.OperationType = model.OperationType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Operation?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Operation>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignRouting(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignRouting(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignWorkCenter(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignWorkCenter(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignInspectionPlan(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignInspectionPlan(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
