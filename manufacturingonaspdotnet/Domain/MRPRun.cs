
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class MRPRun
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? MrprunId { get; set; } 
 public virtual string? RunNumber { get; set; } 
 public virtual DateTime? RunDateTime { get; set; } 
 public virtual int? PlanningHorizonDays { get; set; } 
public virtual Plant? Plant { get; set; } 
public virtual ICollection<PlannedOrder> PlannedOrders { get; set; } = new List<PlannedOrder>();
 public virtual MRPRunStatus? Status { get; set; } 

    public static MRPRun FromRequest(MRPRunRequest request) {
        return new MRPRun {
            Id = request.Id,
            RunNumber = request.RunNumber,
            RunDateTime = request.RunDateTime,
            PlanningHorizonDays = request.PlanningHorizonDays,
            Status = request.Status,
        };
    }
}
