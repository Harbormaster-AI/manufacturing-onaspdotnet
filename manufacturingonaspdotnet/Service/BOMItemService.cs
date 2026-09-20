using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Service;

public interface IBOMItemService {

    Task Create(BOMItem model , CancellationToken cancellationToken);
    Task<bool> Update(BOMItem model, CancellationToken cancellationToken);
    Task<BOMItem?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<BOMItem>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignBom(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignBom(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignComponent(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignComponent(AssociationRequest request, CancellationToken cancellationToken);


}

public class BOMItemService : IBOMItemService
{
    private readonly IBOMItemRepository _repository;
    private readonly ILogger<BOMItemService> _logger;

    public BOMItemService(
        IBOMItemRepository repository, ILogger<BOMItemService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(BOMItem model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(BOMItem model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.LineNumber = model.LineNumber;
            existing.Quantity = model.Quantity;
            existing.ScrapPercent = model.ScrapPercent;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<BOMItem?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<BOMItem>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignBom(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignBom(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignComponent(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignComponent(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
