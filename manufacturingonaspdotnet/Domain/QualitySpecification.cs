
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class QualitySpecification
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? QualityspecificationId { get; set; } 
 public virtual string? SpecCode { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Version { get; set; } 
public virtual Item? Item { get; set; } 

    public static QualitySpecification FromRequest(QualitySpecificationRequest request) {
        return new QualitySpecification {
            Id = request.Id,
            SpecCode = request.SpecCode,
            Name = request.Name,
            Version = request.Version,
        };
    }
}
