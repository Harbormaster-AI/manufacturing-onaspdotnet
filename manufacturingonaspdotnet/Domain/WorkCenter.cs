
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class WorkCenter
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? WorkcenterId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Code { get; set; } 
 public virtual int? CapacityPerHour { get; set; } 
 public virtual Percentage? OeeTarget { get; set; } 
public virtual ProductionLine? ProductionLine { get; set; } 
public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();
public virtual ICollection<MaintenanceOrder> MaintenanceOrders { get; set; } = new List<MaintenanceOrder>();
 public virtual WorkCenterType? WorkCenterType { get; set; } 

    public static WorkCenter FromRequest(WorkCenterRequest request) {
        return new WorkCenter {
            Id = request.Id,
            Name = request.Name,
            Code = request.Code,
            CapacityPerHour = request.CapacityPerHour,
            OeeTarget = request.OeeTarget,
            WorkCenterType = request.WorkCenterType,
        };
    }
}
