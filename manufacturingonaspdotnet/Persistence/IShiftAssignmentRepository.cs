using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IShiftAssignmentRepository
{
    Task<ShiftAssignment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ShiftAssignment>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ShiftAssignment shiftAssignment, CancellationToken cancellationToken);
    Task UpdateAsync(ShiftAssignment shiftAssignment, CancellationToken cancellationToken);
    Task DeleteAsync(ShiftAssignment shiftAssignment, CancellationToken cancellationToken);


}
