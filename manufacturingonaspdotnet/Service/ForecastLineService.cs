using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Service;

public interface IForecastLineService {

    Task Create(ForecastLine model , CancellationToken cancellationToken);
    Task<bool> Update(ForecastLine model, CancellationToken cancellationToken);
    Task<ForecastLine?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ForecastLine>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignForecast(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignForecast(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignItem(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignItem(AssociationRequest request, CancellationToken cancellationToken);


}

public class ForecastLineService : IForecastLineService
{
    private readonly IForecastLineRepository _repository;
    private readonly ILogger<ForecastLineService> _logger;

    public ForecastLineService(
        IForecastLineRepository repository, ILogger<ForecastLineService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(ForecastLine model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(ForecastLine model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Period = model.Period;
            existing.Quantity = model.Quantity;
            existing.Confidence = model.Confidence;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<ForecastLine?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ForecastLine>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignForecast(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignForecast(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignItem(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignItem(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
