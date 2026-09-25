using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Customer customer, CancellationToken cancellationToken);
    Task UpdateAsync(Customer customer, CancellationToken cancellationToken);
    Task DeleteAsync(Customer customer, CancellationToken cancellationToken);

    Task AddToEnterprisesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromEnterprisesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToSalesOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromSalesOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
