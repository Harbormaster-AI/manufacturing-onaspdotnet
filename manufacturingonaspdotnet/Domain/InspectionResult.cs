
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class InspectionResult
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? InspectionresultId { get; set; } 
 public virtual Measurement? ResultValue { get; set; } 
 public virtual DateTime? RecordedOn { get; set; } 
 public virtual string? Notes { get; set; } 
public virtual InspectionLot? InspectionLot { get; set; } 
public virtual InspectionCharacteristic? Characteristic { get; set; } 
 public virtual InspectionResultStatus? ResultStatus { get; set; } 

    public static InspectionResult FromRequest(InspectionResultRequest request) {
        return new InspectionResult {
            Id = request.Id,
            ResultValue = request.ResultValue,
            RecordedOn = request.RecordedOn,
            Notes = request.Notes,
            ResultStatus = request.ResultStatus,
        };
    }
}
