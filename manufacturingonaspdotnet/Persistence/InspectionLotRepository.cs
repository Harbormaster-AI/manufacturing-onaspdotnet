
using manufacturingonaspdotnet.Contracts;
using manufacturingonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace manufacturingonaspdotnet.Persistence;

public class InspectionLotRepository : IInspectionLotRepository
{
    private readonly ApplicationDbContext _db;

    public InspectionLotRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<InspectionLot?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.InspectionLots
            .Include(x => x.Item)
            .Include(x => x.WorkOrder)
            .Include(x => x.GoodsReceipt)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<InspectionLot>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.InspectionLots
            .AsNoTracking()
            .Include(x => x.Item)
            .Include(x => x.WorkOrder)
            .Include(x => x.GoodsReceipt)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(InspectionLot inspectionLot, CancellationToken cancellationToken)
    {
        _db.InspectionLots.Add(inspectionLot);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(InspectionLot inspectionLot, CancellationToken cancellationToken)
    {
        _db.InspectionLots.Update(inspectionLot);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(InspectionLot inspectionLot, CancellationToken cancellationToken)
    {
        _db.InspectionLots.Remove(inspectionLot);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToResultsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.InspectionResults
            .Where(inspectionResult =>
                request.ChildIds.Contains(inspectionResult.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    inspectionResult =>
                        EF.Property<Guid?>(
                            inspectionResult,
                            "PlannedOrder_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromResultsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.InspectionResults
            .Where(inspectionResult =>
                request.ChildIds.Contains(inspectionResult.Id) &&
                EF.Property<Guid?>(
                    inspectionResult,
                    "PlannedOrder_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    inspectionResult =>
                        EF.Property<Guid?>(
                            inspectionResult,
                            "PlannedOrder_Id"),
                    (Guid?)null));
    }

}
