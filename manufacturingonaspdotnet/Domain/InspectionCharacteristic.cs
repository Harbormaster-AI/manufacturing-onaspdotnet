
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class InspectionCharacteristic
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? InspectioncharacteristicId { get; set; } 
 public virtual string? CharacteristicCode { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual Measurement? LowerSpecLimit { get; set; } 
 public virtual Measurement? UpperSpecLimit { get; set; } 
 public virtual Measurement? Target { get; set; } 
public virtual InspectionPlan? InspectionPlan { get; set; } 
 public virtual MeasurementType? MeasurementType { get; set; } 

    public static InspectionCharacteristic FromRequest(InspectionCharacteristicRequest request) {
        return new InspectionCharacteristic {
            Id = request.Id,
            CharacteristicCode = request.CharacteristicCode,
            Name = request.Name,
            LowerSpecLimit = request.LowerSpecLimit,
            UpperSpecLimit = request.UpperSpecLimit,
            Target = request.Target,
            MeasurementType = request.MeasurementType,
        };
    }
}
