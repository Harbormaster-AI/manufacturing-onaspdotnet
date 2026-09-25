
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class InspectionCharacteristicRepository : IInspectionCharacteristicRepository
{
    private readonly ApplicationDbContext _db;

    public InspectionCharacteristicRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<InspectionCharacteristic?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.InspectionCharacteristics
            .Include(x => x.InspectionPlan)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<InspectionCharacteristic>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.InspectionCharacteristics
            .AsNoTracking()
            .Include(x => x.InspectionPlan)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(InspectionCharacteristic inspectionCharacteristic, CancellationToken cancellationToken)
    {
        _db.InspectionCharacteristics.Add(inspectionCharacteristic);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(InspectionCharacteristic inspectionCharacteristic, CancellationToken cancellationToken)
    {
        _db.InspectionCharacteristics.Update(inspectionCharacteristic);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(InspectionCharacteristic inspectionCharacteristic, CancellationToken cancellationToken)
    {
        _db.InspectionCharacteristics.Remove(inspectionCharacteristic);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
