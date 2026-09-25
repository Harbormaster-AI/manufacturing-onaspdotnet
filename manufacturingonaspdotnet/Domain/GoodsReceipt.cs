
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class GoodsReceipt
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? GoodsreceiptId { get; set; } 
 public virtual string? ReceiptNumber { get; set; } 
 public virtual DateOnly? ReceiptDate { get; set; } 
public virtual PurchaseOrder? PurchaseOrder { get; set; } 
public virtual Warehouse? Warehouse { get; set; } 
public virtual ICollection<GoodsReceiptLine> Lines { get; set; } = new List<GoodsReceiptLine>();
 public virtual ReceiptStatus? Status { get; set; } 

    public static GoodsReceipt FromRequest(GoodsReceiptRequest request) {
        return new GoodsReceipt {
            Id = request.Id,
            ReceiptNumber = request.ReceiptNumber,
            ReceiptDate = request.ReceiptDate,
            Status = request.Status,
        };
    }
}
