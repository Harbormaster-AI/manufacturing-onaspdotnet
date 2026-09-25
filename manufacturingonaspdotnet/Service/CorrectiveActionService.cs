
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Persistence;
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Telemetry;

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
    private readonly ApplicationTelemetry _telemetry;
    private readonly ICorrectiveActionRepository _repository;
    private readonly ILogger<CorrectiveActionService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public CorrectiveActionService(
        ApplicationTelemetry telemetry,
        ICorrectiveActionRepository repository,
        ILogger<CorrectiveActionService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(CorrectiveAction model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "CorrectiveAction",
                "CreateCorrectiveAction",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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

            await _telemetry.Execute(
                "CorrectiveAction",
                "UpdateCorrectiveAction",
                () => _repository.UpdateAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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
            await _telemetry.Execute(
                "CorrectiveAction",
                "UpdateCorrectiveAction",
                () => _repository.DeleteAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AssignNonconformance(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No CorrectiveAction found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<NonconformanceService>().Get(childRequest, cancellationToken);
            parent.Nonconformance = child;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> UnassignNonconformance(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No CorrectiveAction found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Nonconformance = null;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AssignOwner(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No CorrectiveAction found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<EmployeeService>().Get(childRequest, cancellationToken);
            parent.Owner = child;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> UnassignOwner(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No CorrectiveAction found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Owner = null;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }




}
