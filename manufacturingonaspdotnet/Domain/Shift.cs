
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class Shift
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ShiftId { get; set; } 
 public virtual string? ShiftName { get; set; } 
 public virtual string? StartTime { get; set; } 
 public virtual string? EndTime { get; set; } 
public virtual Plant? Plant { get; set; } 
public virtual ICollection<ShiftAssignment> Assignments { get; set; } = new List<ShiftAssignment>();
 public virtual ShiftType? ShiftType { get; set; } 

    public static Shift FromRequest(ShiftRequest request) {
        return new Shift {
            Id = request.Id,
            ShiftName = request.ShiftName,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            ShiftType = request.ShiftType,
        };
    }
}
