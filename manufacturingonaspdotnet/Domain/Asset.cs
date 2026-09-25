
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class Asset
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AssetId { get; set; } 
 public virtual string? AssetTag { get; set; } 
 public virtual string? AssetName { get; set; } 
 public virtual DateOnly? CommissioningDate { get; set; } 
public virtual Plant? Plant { get; set; } 
public virtual WorkCenter? WorkCenter { get; set; } 
public virtual ICollection<MaintenanceOrder> MaintenanceOrders { get; set; } = new List<MaintenanceOrder>();
public virtual ICollection<MaintenancePlan> MaintenancePlans { get; set; } = new List<MaintenancePlan>();
 public virtual AssetStatus? AssetStatus { get; set; } 

    public static Asset FromRequest(AssetRequest request) {
        return new Asset {
            Id = request.Id,
            AssetTag = request.AssetTag,
            AssetName = request.AssetName,
            CommissioningDate = request.CommissioningDate,
            AssetStatus = request.AssetStatus,
        };
    }
}
