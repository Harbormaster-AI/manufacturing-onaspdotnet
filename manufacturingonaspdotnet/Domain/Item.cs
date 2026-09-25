
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class Item
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ItemId { get; set; } 
 public virtual string? ItemNumber { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual Money? StandardCost { get; set; } 
 public virtual Measurement? Weight { get; set; } 
 public virtual bool? AsSerialControlled { get; set; } 
public virtual BusinessUnit? BusinessUnit { get; set; } 
public virtual ICollection<BOM> Boms { get; set; } = new List<BOM>();
public virtual ICollection<Routing> Routings { get; set; } = new List<Routing>();
public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
public virtual ICollection<QualitySpecification> QualitySpecifications { get; set; } = new List<QualitySpecification>();
public virtual ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
 public virtual ItemType? ItemType { get; set; } 
 public virtual ProcurementType? ProcurementType { get; set; } 
 public virtual UnitOfMeasure? UnitOfMeasure { get; set; } 
 public virtual ProductLifecycleStatus? LifecycleStatus { get; set; } 

    public static Item FromRequest(ItemRequest request) {
        return new Item {
            Id = request.Id,
            ItemNumber = request.ItemNumber,
            Name = request.Name,
            StandardCost = request.StandardCost,
            Weight = request.Weight,
            AsSerialControlled = request.AsSerialControlled,
            ItemType = request.ItemType,
            ProcurementType = request.ProcurementType,
            UnitOfMeasure = request.UnitOfMeasure,
            LifecycleStatus = request.LifecycleStatus,
        };
    }
}
