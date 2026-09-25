
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class InventoryTransaction
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? InventorytransactionId { get; set; } 
 public virtual string? TransactionNumber { get; set; } 
 public virtual Quantity? Quantity { get; set; } 
 public virtual DateTime? TransactionDateTime { get; set; } 
 public virtual string? ReferenceDocument { get; set; } 
public virtual Item? Item { get; set; } 
public virtual Location? Location { get; set; } 
public virtual WorkOrder? WorkOrder { get; set; } 
public virtual PurchaseOrder? PurchaseOrder { get; set; } 
public virtual SalesOrder? SalesOrder { get; set; } 
 public virtual InventoryTransactionType? TransactionType { get; set; } 

    public static InventoryTransaction FromRequest(InventoryTransactionRequest request) {
        return new InventoryTransaction {
            Id = request.Id,
            TransactionNumber = request.TransactionNumber,
            Quantity = request.Quantity,
            TransactionDateTime = request.TransactionDateTime,
            ReferenceDocument = request.ReferenceDocument,
            TransactionType = request.TransactionType,
        };
    }
}
