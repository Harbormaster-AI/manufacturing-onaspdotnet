
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class ForecastLine
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ForecastlineId { get; set; } 
 public virtual DateOnly? Period { get; set; } 
 public virtual Quantity? Quantity { get; set; } 
 public virtual Percentage? Confidence { get; set; } 
public virtual Forecast? Forecast { get; set; } 
public virtual Item? Item { get; set; } 

    public static ForecastLine FromRequest(ForecastLineRequest request) {
        return new ForecastLine {
            Id = request.Id,
            Period = request.Period,
            Quantity = request.Quantity,
            Confidence = request.Confidence,
        };
    }
}
