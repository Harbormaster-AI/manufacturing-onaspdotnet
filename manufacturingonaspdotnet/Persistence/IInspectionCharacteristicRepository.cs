using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IInspectionCharacteristicRepository
{
    Task<InspectionCharacteristic?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<InspectionCharacteristic>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(InspectionCharacteristic inspectionCharacteristic, CancellationToken cancellationToken);
    Task UpdateAsync(InspectionCharacteristic inspectionCharacteristic, CancellationToken cancellationToken);
    Task DeleteAsync(InspectionCharacteristic inspectionCharacteristic, CancellationToken cancellationToken);


}
