
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class Employee
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? EmployeeId { get; set; } 
 public virtual string? FirstName { get; set; } 
 public virtual string? LastName { get; set; } 
public virtual WorkCenter? WorkCenter { get; set; } 
public virtual ICollection<ShiftAssignment> ShiftAssignments { get; set; } = new List<ShiftAssignment>();
public virtual ICollection<CorrectiveAction> CorrectiveActions { get; set; } = new List<CorrectiveAction>();
 public virtual EmployeeRole? Role { get; set; } 
 public virtual SkillLevel? SkillLevel { get; set; } 

    public static Employee FromRequest(EmployeeRequest request) {
        return new Employee {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Role = request.Role,
            SkillLevel = request.SkillLevel,
        };
    }
}
