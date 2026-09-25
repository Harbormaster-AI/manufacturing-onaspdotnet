
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class SalesOrder
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? SalesorderId { get; set; } 
 public virtual string? OrderNumber { get; set; } 
 public virtual DateOnly? OrderDate { get; set; } 
 public virtual Money? TotalAmount { get; set; } 
public virtual Customer? Customer { get; set; } 
public virtual Plant? Plant { get; set; } 
public virtual ICollection<SalesOrderLine> Lines { get; set; } = new List<SalesOrderLine>();
public virtual ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
 public virtual SalesOrderStatus? Status { get; set; } 

    public static SalesOrder FromRequest(SalesOrderRequest request) {
        return new SalesOrder {
            Id = request.Id,
            OrderNumber = request.OrderNumber,
            OrderDate = request.OrderDate,
            TotalAmount = request.TotalAmount,
            Status = request.Status,
        };
    }
}
