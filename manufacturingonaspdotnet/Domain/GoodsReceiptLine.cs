
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class GoodsReceiptLine
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? GoodsreceiptlineId { get; set; } 
 public virtual int? LineNumber { get; set; } 
 public virtual Quantity? ReceivedQuantity { get; set; } 
 public virtual Quantity? AcceptedQuantity { get; set; } 
 public virtual Quantity? RejectedQuantity { get; set; } 
 public virtual LotId? Lot { get; set; } 
public virtual GoodsReceipt? GoodsReceipt { get; set; } 
public virtual Item? Item { get; set; } 
public virtual InventoryTransaction? InventoryTransaction { get; set; } 

    public static GoodsReceiptLine FromRequest(GoodsReceiptLineRequest request) {
        return new GoodsReceiptLine {
            Id = request.Id,
            LineNumber = request.LineNumber,
            ReceivedQuantity = request.ReceivedQuantity,
            AcceptedQuantity = request.AcceptedQuantity,
            RejectedQuantity = request.RejectedQuantity,
            Lot = request.Lot,
        };
    }
}
