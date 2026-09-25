
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class Enterprise
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? EnterpriseId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? LegalName { get; set; } 
 public virtual string? RegistrationCountry { get; set; } 
 public virtual string? Website { get; set; } 
 public virtual string? TaxId { get; set; } 
public virtual ICollection<BusinessUnit> BusinessUnits { get; set; } = new List<BusinessUnit>();
public virtual ICollection<Plant> Plants { get; set; } = new List<Plant>();
public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public static Enterprise FromRequest(EnterpriseRequest request) {
        return new Enterprise {
            Id = request.Id,
            Name = request.Name,
            LegalName = request.LegalName,
            RegistrationCountry = request.RegistrationCountry,
            Website = request.Website,
            TaxId = request.TaxId,
        };
    }
}
