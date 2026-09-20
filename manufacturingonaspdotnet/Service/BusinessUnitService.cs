using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Service;

public interface IBusinessUnitService {

    Task Create(BusinessUnit model , CancellationToken cancellationToken);
    Task<bool> Update(BusinessUnit model, CancellationToken cancellationToken);
    Task<BusinessUnit?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<BusinessUnit>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignEnterprise(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignEnterprise(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToItems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromItems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPlants(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPlants(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class BusinessUnitService : IBusinessUnitService
{
    private readonly IBusinessUnitRepository _repository;
    private readonly ILogger<BusinessUnitService> _logger;

    public BusinessUnitService(
        IBusinessUnitRepository repository, ILogger<BusinessUnitService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(BusinessUnit model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(BusinessUnit model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Code = model.Code;
            existing.Category = model.Category;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<BusinessUnit?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<BusinessUnit>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignEnterprise(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignEnterprise(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToPlants(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPlants(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
