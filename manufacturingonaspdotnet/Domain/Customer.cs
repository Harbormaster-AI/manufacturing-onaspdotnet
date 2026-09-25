
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class Customer
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CustomerId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? CustomerCode { get; set; } 
 public virtual Address? Address { get; set; } 
public virtual ICollection<Enterprise> Enterprises { get; set; } = new List<Enterprise>();
public virtual ICollection<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();
 public virtual CustomerType? CustomerType { get; set; } 

    public static Customer FromRequest(CustomerRequest request) {
        return new Customer {
            Id = request.Id,
            Name = request.Name,
            CustomerCode = request.CustomerCode,
            Address = request.Address,
            CustomerType = request.CustomerType,
        };
    }
}
