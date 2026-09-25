
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class Supplier
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? SupplierId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? SupplierCode { get; set; } 
 public virtual Address? Address { get; set; } 
public virtual ICollection<Enterprise> Enterprises { get; set; } = new List<Enterprise>();
public virtual ICollection<Item> Items { get; set; } = new List<Item>();
public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
 public virtual SupplierTier? SupplierTier { get; set; } 
 public virtual PaymentTerms? PaymentTerms { get; set; } 

    public static Supplier FromRequest(SupplierRequest request) {
        return new Supplier {
            Id = request.Id,
            Name = request.Name,
            SupplierCode = request.SupplierCode,
            Address = request.Address,
            SupplierTier = request.SupplierTier,
            PaymentTerms = request.PaymentTerms,
        };
    }
}
