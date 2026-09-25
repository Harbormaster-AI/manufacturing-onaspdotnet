
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class Nonconformance
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? NonconformanceId { get; set; } 
 public virtual string? NcNumber { get; set; } 
 public virtual string? Description { get; set; } 
 public virtual string? ContainmentAction { get; set; } 
public virtual Item? Item { get; set; } 
public virtual WorkOrder? WorkOrder { get; set; } 
public virtual InspectionLot? InspectionLot { get; set; } 
public virtual CorrectiveAction? CorrectiveAction { get; set; } 
 public virtual NonconformanceType? NcType { get; set; } 
 public virtual QualitySeverity? Severity { get; set; } 
 public virtual NonconformanceStatus? Status { get; set; } 

    public static Nonconformance FromRequest(NonconformanceRequest request) {
        return new Nonconformance {
            Id = request.Id,
            NcNumber = request.NcNumber,
            Description = request.Description,
            ContainmentAction = request.ContainmentAction,
            NcType = request.NcType,
            Severity = request.Severity,
            Status = request.Status,
        };
    }
}
