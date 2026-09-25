
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class ProductionLine
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ProductionlineId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? LineCode { get; set; } 
public virtual Plant? Plant { get; set; } 
public virtual ICollection<WorkCenter> WorkCenters { get; set; } = new List<WorkCenter>();
 public virtual ProductionLineType? LineType { get; set; } 

    public static ProductionLine FromRequest(ProductionLineRequest request) {
        return new ProductionLine {
            Id = request.Id,
            Name = request.Name,
            LineCode = request.LineCode,
            LineType = request.LineType,
        };
    }
}
