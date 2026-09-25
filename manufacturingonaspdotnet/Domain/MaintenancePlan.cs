
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class MaintenancePlan
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? MaintenanceplanId { get; set; } 
 public virtual string? PlanNumber { get; set; } 
 public virtual TimeDuration? Interval { get; set; } 
 public virtual DateOnly? LastServiceDate { get; set; } 
public virtual Asset? Asset { get; set; } 
public virtual ICollection<MaintenanceOrder> MaintenanceOrders { get; set; } = new List<MaintenanceOrder>();
 public virtual MaintenanceStrategy? Strategy { get; set; } 

    public static MaintenancePlan FromRequest(MaintenancePlanRequest request) {
        return new MaintenancePlan {
            Id = request.Id,
            PlanNumber = request.PlanNumber,
            Interval = request.Interval,
            LastServiceDate = request.LastServiceDate,
            Strategy = request.Strategy,
        };
    }
}
