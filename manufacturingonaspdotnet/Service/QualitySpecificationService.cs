using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Service;

public interface IQualitySpecificationService {

    Task Create(QualitySpecification model , CancellationToken cancellationToken);
    Task<bool> Update(QualitySpecification model, CancellationToken cancellationToken);
    Task<QualitySpecification?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<QualitySpecification>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignItem(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignItem(AssociationRequest request, CancellationToken cancellationToken);


}

public class QualitySpecificationService : IQualitySpecificationService
{
    private readonly IQualitySpecificationRepository _repository;
    private readonly ILogger<QualitySpecificationService> _logger;

    public QualitySpecificationService(
        IQualitySpecificationRepository repository, ILogger<QualitySpecificationService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(QualitySpecification model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(QualitySpecification model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.SpecCode = model.SpecCode;
            existing.Name = model.Name;
            existing.Version = model.Version;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<QualitySpecification?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<QualitySpecification>> GetAll(CancellationToken cancellationToken)
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




}
