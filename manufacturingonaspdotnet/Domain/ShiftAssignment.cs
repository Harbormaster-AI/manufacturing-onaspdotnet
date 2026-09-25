
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class ShiftAssignment
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ShiftassignmentId { get; set; } 
 public virtual DateOnly? AssignmentDate { get; set; } 
public virtual Shift? Shift { get; set; } 
public virtual Employee? Employee { get; set; } 
public virtual WorkCenter? WorkCenter { get; set; } 

    public static ShiftAssignment FromRequest(ShiftAssignmentRequest request) {
        return new ShiftAssignment {
            Id = request.Id,
            AssignmentDate = request.AssignmentDate,
        };
    }
}
