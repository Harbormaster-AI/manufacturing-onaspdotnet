using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IEmployeeRepository
{
    Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Employee>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Employee employee, CancellationToken cancellationToken);
    Task UpdateAsync(Employee employee, CancellationToken cancellationToken);
    Task DeleteAsync(Employee employee, CancellationToken cancellationToken);

    Task AddToShiftAssignmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromShiftAssignmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToCorrectiveActionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCorrectiveActionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
