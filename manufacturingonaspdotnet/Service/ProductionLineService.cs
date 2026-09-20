using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Service;

public interface IProductionLineService {

    Task Create(ProductionLine model , CancellationToken cancellationToken);
    Task<bool> Update(ProductionLine model, CancellationToken cancellationToken);
    Task<ProductionLine?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ProductionLine>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignPlant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPlant(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToWorkCenters(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromWorkCenters(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class ProductionLineService : IProductionLineService
{
    private readonly IProductionLineRepository _repository;
    private readonly ILogger<ProductionLineService> _logger;

    public ProductionLineService(
        IProductionLineRepository repository, ILogger<ProductionLineService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(ProductionLine model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(ProductionLine model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.LineCode = model.LineCode;
            existing.LineType = model.LineType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<ProductionLine?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ProductionLine>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToWorkCenters(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromWorkCenters(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
