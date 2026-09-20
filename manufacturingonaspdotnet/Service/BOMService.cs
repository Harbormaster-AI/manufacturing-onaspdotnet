using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Service;

public interface IBOMService {

    Task Create(BOM model , CancellationToken cancellationToken);
    Task<bool> Update(BOM model, CancellationToken cancellationToken);
    Task<BOM?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<BOM>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignParentItem(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignParentItem(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToBomItems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromBomItems(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class BOMService : IBOMService
{
    private readonly IBOMRepository _repository;
    private readonly ILogger<BOMService> _logger;

    public BOMService(
        IBOMRepository repository, ILogger<BOMService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(BOM model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(BOM model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.BomNumber = model.BomNumber;
            existing.Revision = model.Revision;
            existing.EffectivityStart = model.EffectivityStart;
            existing.EffectivityEnd = model.EffectivityEnd;
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

    public Task<BOM?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<BOM>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignParentItem(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignParentItem(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToBomItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromBomItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
