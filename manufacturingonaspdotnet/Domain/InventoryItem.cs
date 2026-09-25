
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class InventoryItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? InventoryitemId { get; set; } 
 public virtual Quantity? QuantityOnHand { get; set; } 
 public virtual Quantity? QuantityReserved { get; set; } 
 public virtual LotId? LotNumber { get; set; } 
 public virtual SerialId? SerialNumber { get; set; } 
public virtual Item? Item { get; set; } 
public virtual Location? Location { get; set; } 

    public static InventoryItem FromRequest(InventoryItemRequest request) {
        return new InventoryItem {
            Id = request.Id,
            QuantityOnHand = request.QuantityOnHand,
            QuantityReserved = request.QuantityReserved,
            LotNumber = request.LotNumber,
            SerialNumber = request.SerialNumber,
        };
    }
}
