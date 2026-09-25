
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _db;

    public EmployeeRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Employees
            .Include(x => x.WorkCenter)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Employee>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Employees
            .AsNoTracking()
            .Include(x => x.WorkCenter)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Employee employee, CancellationToken cancellationToken)
    {
        _db.Employees.Add(employee);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Employee employee, CancellationToken cancellationToken)
    {
        _db.Employees.Update(employee);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Employee employee, CancellationToken cancellationToken)
    {
        _db.Employees.Remove(employee);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToShiftAssignmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ShiftAssignments
            .Where(shiftAssignment =>
                request.ChildIds.Contains(shiftAssignment.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    shiftAssignment =>
                        EF.Property<Guid?>(
                            shiftAssignment,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromShiftAssignmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ShiftAssignments
            .Where(shiftAssignment =>
                request.ChildIds.Contains(shiftAssignment.Id) &&
                EF.Property<Guid?>(
                    shiftAssignment,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    shiftAssignment =>
                        EF.Property<Guid?>(
                            shiftAssignment,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }


    public async Task AddToCorrectiveActionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CorrectiveActions
            .Where(correctiveAction =>
                request.ChildIds.Contains(correctiveAction.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    correctiveAction =>
                        EF.Property<Guid?>(
                            correctiveAction,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCorrectiveActionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CorrectiveActions
            .Where(correctiveAction =>
                request.ChildIds.Contains(correctiveAction.Id) &&
                EF.Property<Guid?>(
                    correctiveAction,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    correctiveAction =>
                        EF.Property<Guid?>(
                            correctiveAction,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }

}
