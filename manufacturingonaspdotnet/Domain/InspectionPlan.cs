
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class InspectionPlan
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? InspectionplanId { get; set; } 
 public virtual string? PlanNumber { get; set; } 
 public virtual string? Revision { get; set; } 
public virtual Item? Item { get; set; } 
public virtual ICollection<InspectionCharacteristic> Characteristics { get; set; } = new List<InspectionCharacteristic>();
 public virtual SamplingPlanType? SamplingPlan { get; set; } 
 public virtual QualityPlanStatus? Status { get; set; } 

    public static InspectionPlan FromRequest(InspectionPlanRequest request) {
        return new InspectionPlan {
            Id = request.Id,
            PlanNumber = request.PlanNumber,
            Revision = request.Revision,
            SamplingPlan = request.SamplingPlan,
            Status = request.Status,
        };
    }
}
