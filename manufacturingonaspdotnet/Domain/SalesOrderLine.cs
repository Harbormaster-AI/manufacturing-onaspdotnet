
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class SalesOrderLine
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? SalesorderlineId { get; set; } 
 public virtual int? LineNumber { get; set; } 
 public virtual Quantity? Quantity { get; set; } 
 public virtual Money? UnitPrice { get; set; } 
 public virtual DateOnly? DueDate { get; set; } 
public virtual SalesOrder? SalesOrder { get; set; } 
public virtual Item? Item { get; set; } 

    public static SalesOrderLine FromRequest(SalesOrderLineRequest request) {
        return new SalesOrderLine {
            Id = request.Id,
            LineNumber = request.LineNumber,
            Quantity = request.Quantity,
            UnitPrice = request.UnitPrice,
            DueDate = request.DueDate,
        };
    }
}
