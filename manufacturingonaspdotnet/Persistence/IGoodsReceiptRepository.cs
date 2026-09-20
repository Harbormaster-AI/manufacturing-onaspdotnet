using manufacturingonaspdotnet.Domain;

namespace manufacturingonaspdotnet.Persistence;

public interface IGoodsReceiptRepository
{
    Task<GoodsReceipt?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<GoodsReceipt>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(GoodsReceipt goodsReceipt, CancellationToken cancellationToken);
    Task UpdateAsync(GoodsReceipt goodsReceipt, CancellationToken cancellationToken);
    Task DeleteAsync(GoodsReceipt goodsReceipt, CancellationToken cancellationToken);
}
