
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class Warehouse
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? WarehouseId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? WarehouseCode { get; set; } 
 public virtual Address? Address { get; set; } 
public virtual Plant? Plant { get; set; } 
public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
public virtual ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
 public virtual WarehouseType? WarehouseType { get; set; } 

    public static Warehouse FromRequest(WarehouseRequest request) {
        return new Warehouse {
            Id = request.Id,
            Name = request.Name,
            WarehouseCode = request.WarehouseCode,
            Address = request.Address,
            WarehouseType = request.WarehouseType,
        };
    }
}
