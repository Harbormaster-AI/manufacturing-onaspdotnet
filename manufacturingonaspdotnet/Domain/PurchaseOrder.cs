
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class PurchaseOrder
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PurchaseorderId { get; set; } 
 public virtual string? PoNumber { get; set; } 
 public virtual DateOnly? OrderDate { get; set; } 
 public virtual Money? TotalAmount { get; set; } 
public virtual Supplier? Supplier { get; set; } 
public virtual Plant? Plant { get; set; } 
public virtual ICollection<PurchaseOrderLine> Lines { get; set; } = new List<PurchaseOrderLine>();
public virtual ICollection<GoodsReceipt> GoodsReceipts { get; set; } = new List<GoodsReceipt>();
 public virtual PurchaseOrderStatus? Status { get; set; } 

    public static PurchaseOrder FromRequest(PurchaseOrderRequest request) {
        return new PurchaseOrder {
            Id = request.Id,
            PoNumber = request.PoNumber,
            OrderDate = request.OrderDate,
            TotalAmount = request.TotalAmount,
            Status = request.Status,
        };
    }
}
