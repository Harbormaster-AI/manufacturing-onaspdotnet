using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Service;

public interface ICorrectiveActionService {

    Task Create(CorrectiveAction model , CancellationToken cancellationToken);
    Task<bool> Update(CorrectiveAction model, CancellationToken cancellationToken);
    Task<CorrectiveAction?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<CorrectiveAction>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignNonconformance(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignNonconformance(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignOwner(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignOwner(AssociationRequest request, CancellationToken cancellationToken);


}

public class CorrectiveActionService : ICorrectiveActionService
{
    private readonly ICorrectiveActionRepository _repository;
    private readonly ILogger<CorrectiveActionService> _logger;

    public CorrectiveActionService(
        ICorrectiveActionRepository repository, ILogger<CorrectiveActionService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(CorrectiveAction model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(CorrectiveAction model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.CapaNumber = model.CapaNumber;
            existing.RootCause = model.RootCause;
            existing.CorrectiveAction_ = model.CorrectiveAction_;
            existing.VerificationDate = model.VerificationDate;
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

    public Task<CorrectiveAction?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<CorrectiveAction>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignNonconformance(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignNonconformance(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignOwner(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignOwner(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
