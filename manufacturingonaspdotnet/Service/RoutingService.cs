using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Service;

public interface IRoutingService {

    Task Create(Routing model , CancellationToken cancellationToken);
    Task<bool> Update(Routing model, CancellationToken cancellationToken);
    Task<Routing?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Routing>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignItem(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignItem(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToOperations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromOperations(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class RoutingService : IRoutingService
{
    private readonly IRoutingRepository _repository;
    private readonly ILogger<RoutingService> _logger;

    public RoutingService(
        IRoutingRepository repository, ILogger<RoutingService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Routing model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Routing model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.RoutingNumber = model.RoutingNumber;
            existing.Revision = model.Revision;
            existing.EffectivityStart = model.EffectivityStart;
            existing.EffectivityEnd = model.EffectivityEnd;
            existing.RoutingType = model.RoutingType;
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

    public Task<Routing?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Routing>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToOperations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromOperations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
