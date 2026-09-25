
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class WorkOrder
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? WorkorderId { get; set; } 
 public virtual string? WorkOrderNumber { get; set; } 
 public virtual DateTime? PlannedStart { get; set; } 
 public virtual DateTime? PlannedEnd { get; set; } 
 public virtual Quantity? Quantity { get; set; } 
 public virtual int? Priority { get; set; } 
public virtual Item? Item { get; set; } 
public virtual Plant? Plant { get; set; } 
public virtual Routing? Routing { get; set; } 
public virtual BOM? Bom { get; set; } 
public virtual ProductionSchedule? ProductionSchedule { get; set; } 
public virtual SalesOrder? SalesOrder { get; set; } 
 public virtual WorkOrderStatus? Status { get; set; } 

    public static WorkOrder FromRequest(WorkOrderRequest request) {
        return new WorkOrder {
            Id = request.Id,
            WorkOrderNumber = request.WorkOrderNumber,
            PlannedStart = request.PlannedStart,
            PlannedEnd = request.PlannedEnd,
            Quantity = request.Quantity,
            Priority = request.Priority,
            Status = request.Status,
        };
    }
}
