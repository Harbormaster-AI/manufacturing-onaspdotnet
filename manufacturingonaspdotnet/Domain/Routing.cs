
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class Routing
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? RoutingId { get; set; } 
 public virtual string? RoutingNumber { get; set; } 
 public virtual string? Revision { get; set; } 
 public virtual DateOnly? EffectivityStart { get; set; } 
 public virtual DateOnly? EffectivityEnd { get; set; } 
public virtual Item? Item { get; set; } 
public virtual ICollection<Operation> Operations { get; set; } = new List<Operation>();
 public virtual RoutingType? RoutingType { get; set; } 
 public virtual RoutingStatus? Status { get; set; } 

    public static Routing FromRequest(RoutingRequest request) {
        return new Routing {
            Id = request.Id,
            RoutingNumber = request.RoutingNumber,
            Revision = request.Revision,
            EffectivityStart = request.EffectivityStart,
            EffectivityEnd = request.EffectivityEnd,
            RoutingType = request.RoutingType,
            Status = request.Status,
        };
    }
}
