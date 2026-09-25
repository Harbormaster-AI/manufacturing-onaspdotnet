
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class PlannedOrder
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PlannedorderId { get; set; } 
 public virtual string? PlannedOrderNumber { get; set; } 
 public virtual Quantity? Quantity { get; set; } 
 public virtual DateOnly? DueDate { get; set; } 
public virtual MRPRun? MrpRun { get; set; } 
public virtual Item? Item { get; set; } 
public virtual Plant? Plant { get; set; } 
 public virtual PlannedOrderType? OrderType { get; set; } 
 public virtual PlannedOrderStatus? Status { get; set; } 

    public static PlannedOrder FromRequest(PlannedOrderRequest request) {
        return new PlannedOrder {
            Id = request.Id,
            PlannedOrderNumber = request.PlannedOrderNumber,
            Quantity = request.Quantity,
            DueDate = request.DueDate,
            OrderType = request.OrderType,
            Status = request.Status,
        };
    }
}
