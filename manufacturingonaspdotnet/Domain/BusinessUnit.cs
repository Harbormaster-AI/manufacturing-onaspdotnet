
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class BusinessUnit
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? BusinessunitId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Code { get; set; } 
public virtual Enterprise? Enterprise { get; set; } 
public virtual ICollection<Item> Items { get; set; } = new List<Item>();
public virtual ICollection<Plant> Plants { get; set; } = new List<Plant>();
 public virtual BusinessUnitCategory? Category { get; set; } 

    public static BusinessUnit FromRequest(BusinessUnitRequest request) {
        return new BusinessUnit {
            Id = request.Id,
            Name = request.Name,
            Code = request.Code,
            Category = request.Category,
        };
    }
}
