
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class CorrectiveAction
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CorrectiveactionId { get; set; } 
 public virtual string? CapaNumber { get; set; } 
 public virtual string? RootCause { get; set; } 
 public virtual string? CorrectiveAction_ { get; set; } 
 public virtual DateOnly? VerificationDate { get; set; } 
public virtual Nonconformance? Nonconformance { get; set; } 
public virtual Employee? Owner { get; set; } 
 public virtual CAPAStatus? Status { get; set; } 

    public static CorrectiveAction FromRequest(CorrectiveActionRequest request) {
        return new CorrectiveAction {
            Id = request.Id,
            CapaNumber = request.CapaNumber,
            RootCause = request.RootCause,
            CorrectiveAction_ = request.CorrectiveAction_,
            VerificationDate = request.VerificationDate,
            Status = request.Status,
        };
    }
}
