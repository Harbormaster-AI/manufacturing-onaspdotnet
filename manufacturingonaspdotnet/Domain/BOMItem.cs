
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class BOMItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? BomitemId { get; set; } 
 public virtual int? LineNumber { get; set; } 
 public virtual Quantity? Quantity { get; set; } 
 public virtual Percentage? ScrapPercent { get; set; } 
public virtual BOM? Bom { get; set; } 
public virtual Item? Component { get; set; } 

    public static BOMItem FromRequest(BOMItemRequest request) {
        return new BOMItem {
            Id = request.Id,
            LineNumber = request.LineNumber,
            Quantity = request.Quantity,
            ScrapPercent = request.ScrapPercent,
        };
    }
}
