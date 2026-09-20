using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Service;

public interface IPlantService {

    Task Create(Plant model , CancellationToken cancellationToken);
    Task<bool> Update(Plant model, CancellationToken cancellationToken);
    Task<Plant?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Plant>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignEnterprise(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignEnterprise(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToProductionLines(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromProductionLines(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToWorkCenters(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromWorkCenters(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToWarehouses(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromWarehouses(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToAssets(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAssets(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToProductionSchedules(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromProductionSchedules(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class PlantService : IPlantService
{
    private readonly IPlantRepository _repository;
    private readonly ILogger<PlantService> _logger;

    public PlantService(
        IPlantRepository repository, ILogger<PlantService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Plant model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Plant model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.PlantCode = model.PlantCode;
            existing.Address = model.Address;
            existing.TimeZone = model.TimeZone;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Plant?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Plant>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToProductionLines(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromProductionLines(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToWorkCenters(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromWorkCenters(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToWarehouses(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromWarehouses(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToAssets(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromAssets(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToProductionSchedules(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromProductionSchedules(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
