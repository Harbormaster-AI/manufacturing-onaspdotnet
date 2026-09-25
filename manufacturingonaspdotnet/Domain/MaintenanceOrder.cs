
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class MaintenanceOrder
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? MaintenanceorderId { get; set; } 
 public virtual string? OrderNumber { get; set; } 
 public virtual int? Priority { get; set; } 
 public virtual DateOnly? RequestedDate { get; set; } 
 public virtual DateOnly? CompletionDate { get; set; } 
public virtual Asset? Asset { get; set; } 
public virtual MaintenancePlan? Plan { get; set; } 
public virtual WorkCenter? WorkCenter { get; set; } 
 public virtual MaintenanceOrderStatus? Status { get; set; } 

    public static MaintenanceOrder FromRequest(MaintenanceOrderRequest request) {
        return new MaintenanceOrder {
            Id = request.Id,
            OrderNumber = request.OrderNumber,
            Priority = request.Priority,
            RequestedDate = request.RequestedDate,
            CompletionDate = request.CompletionDate,
            Status = request.Status,
        };
    }
}
