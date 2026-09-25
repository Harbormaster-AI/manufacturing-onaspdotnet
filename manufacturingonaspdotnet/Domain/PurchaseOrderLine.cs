
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class PurchaseOrderLine
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PurchaseorderlineId { get; set; } 
 public virtual int? LineNumber { get; set; } 
 public virtual Quantity? Quantity { get; set; } 
 public virtual Money? UnitPrice { get; set; } 
 public virtual DateOnly? DueDate { get; set; } 
public virtual PurchaseOrder? PurchaseOrder { get; set; } 
public virtual Item? Item { get; set; } 

    public static PurchaseOrderLine FromRequest(PurchaseOrderLineRequest request) {
        return new PurchaseOrderLine {
            Id = request.Id,
            LineNumber = request.LineNumber,
            Quantity = request.Quantity,
            UnitPrice = request.UnitPrice,
            DueDate = request.DueDate,
        };
    }
}
