using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Service;

public interface IEnterpriseService {

    Task Create(Enterprise model , CancellationToken cancellationToken);
    Task<bool> Update(Enterprise model, CancellationToken cancellationToken);
    Task<Enterprise?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Enterprise>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToBusinessUnits(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromBusinessUnits(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPlants(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPlants(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToSuppliers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromSuppliers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCustomers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCustomers(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class EnterpriseService : IEnterpriseService
{
    private readonly IEnterpriseRepository _repository;
    private readonly ILogger<EnterpriseService> _logger;

    public EnterpriseService(
        IEnterpriseRepository repository, ILogger<EnterpriseService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Enterprise model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Enterprise model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.LegalName = model.LegalName;
            existing.RegistrationCountry = model.RegistrationCountry;
            existing.Website = model.Website;
            existing.TaxId = model.TaxId;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Enterprise?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Enterprise>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToBusinessUnits(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromBusinessUnits(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToPlants(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPlants(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToSuppliers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromSuppliers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToCustomers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCustomers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
