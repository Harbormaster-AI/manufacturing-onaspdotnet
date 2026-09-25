
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class Operation
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? OperationId { get; set; } 
 public virtual string? OperationNumber { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual TimeDuration? SetupTime { get; set; } 
 public virtual TimeDuration? StandardCycleTime { get; set; } 
public virtual Routing? Routing { get; set; } 
public virtual WorkCenter? WorkCenter { get; set; } 
public virtual InspectionPlan? InspectionPlan { get; set; } 
 public virtual OperationType? OperationType { get; set; } 

    public static Operation FromRequest(OperationRequest request) {
        return new Operation {
            Id = request.Id,
            OperationNumber = request.OperationNumber,
            Name = request.Name,
            SetupTime = request.SetupTime,
            StandardCycleTime = request.StandardCycleTime,
            OperationType = request.OperationType,
        };
    }
}
