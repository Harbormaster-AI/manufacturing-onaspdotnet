
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class BOM
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? BomId { get; set; } 
 public virtual string? BomNumber { get; set; } 
 public virtual string? Revision { get; set; } 
 public virtual DateOnly? EffectivityStart { get; set; } 
 public virtual DateOnly? EffectivityEnd { get; set; } 
public virtual Item? ParentItem { get; set; } 
public virtual ICollection<BOMItem> BomItems { get; set; } = new List<BOMItem>();
 public virtual BOMStatus? Status { get; set; } 

    public static BOM FromRequest(BOMRequest request) {
        return new BOM {
            Id = request.Id,
            BomNumber = request.BomNumber,
            Revision = request.Revision,
            EffectivityStart = request.EffectivityStart,
            EffectivityEnd = request.EffectivityEnd,
            Status = request.Status,
        };
    }
}
