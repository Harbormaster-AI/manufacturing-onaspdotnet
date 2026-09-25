
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class Plant
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PlantId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? PlantCode { get; set; } 
 public virtual Address? Address { get; set; } 
 public virtual string? TimeZone { get; set; } 
public virtual Enterprise? Enterprise { get; set; } 
public virtual ICollection<ProductionLine> ProductionLines { get; set; } = new List<ProductionLine>();
public virtual ICollection<WorkCenter> WorkCenters { get; set; } = new List<WorkCenter>();
public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();
public virtual ICollection<ProductionSchedule> ProductionSchedules { get; set; } = new List<ProductionSchedule>();

    public static Plant FromRequest(PlantRequest request) {
        return new Plant {
            Id = request.Id,
            Name = request.Name,
            PlantCode = request.PlantCode,
            Address = request.Address,
            TimeZone = request.TimeZone,
        };
    }
}
