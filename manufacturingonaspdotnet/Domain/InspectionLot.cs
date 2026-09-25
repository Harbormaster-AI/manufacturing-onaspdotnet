
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class InspectionLot
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? InspectionlotId { get; set; } 
 public virtual string? LotNumber { get; set; } 
 public virtual Quantity? Quantity { get; set; } 
 public virtual int? SampleSize { get; set; } 
 public virtual DateTime? CreatedOn { get; set; } 
public virtual Item? Item { get; set; } 
public virtual WorkOrder? WorkOrder { get; set; } 
public virtual GoodsReceipt? GoodsReceipt { get; set; } 
public virtual ICollection<InspectionResult> Results { get; set; } = new List<InspectionResult>();
 public virtual InspectionType? InspectionType { get; set; } 
 public virtual InspectionStatus? Status { get; set; } 

    public static InspectionLot FromRequest(InspectionLotRequest request) {
        return new InspectionLot {
            Id = request.Id,
            LotNumber = request.LotNumber,
            Quantity = request.Quantity,
            SampleSize = request.SampleSize,
            CreatedOn = request.CreatedOn,
            InspectionType = request.InspectionType,
            Status = request.Status,
        };
    }
}
