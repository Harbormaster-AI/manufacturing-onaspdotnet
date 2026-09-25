
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class Location
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? LocationId { get; set; } 
 public virtual string? LocationCode { get; set; } 
 public virtual string? Description { get; set; } 
public virtual Warehouse? Warehouse { get; set; } 
public virtual ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
 public virtual LocationType? LocationType { get; set; } 

    public static Location FromRequest(LocationRequest request) {
        return new Location {
            Id = request.Id,
            LocationCode = request.LocationCode,
            Description = request.Description,
            LocationType = request.LocationType,
        };
    }
}
