
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class ProductionSchedule
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ProductionscheduleId { get; set; } 
 public virtual string? ScheduleNumber { get; set; } 
 public virtual DateOnly? HorizonStart { get; set; } 
 public virtual DateOnly? HorizonEnd { get; set; } 
public virtual Plant? Plant { get; set; } 
public virtual ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
 public virtual ScheduleStatus? Status { get; set; } 

    public static ProductionSchedule FromRequest(ProductionScheduleRequest request) {
        return new ProductionSchedule {
            Id = request.Id,
            ScheduleNumber = request.ScheduleNumber,
            HorizonStart = request.HorizonStart,
            HorizonEnd = request.HorizonEnd,
            Status = request.Status,
        };
    }
}
