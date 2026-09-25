using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Persistence;

public interface IGoodsReceiptLineRepository
{
    Task<GoodsReceiptLine?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<GoodsReceiptLine>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(GoodsReceiptLine goodsReceiptLine, CancellationToken cancellationToken);
    Task UpdateAsync(GoodsReceiptLine goodsReceiptLine, CancellationToken cancellationToken);
    Task DeleteAsync(GoodsReceiptLine goodsReceiptLine, CancellationToken cancellationToken);


}
