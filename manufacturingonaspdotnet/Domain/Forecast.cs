
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Domain;

public class Forecast
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ForecastId { get; set; } 
 public virtual string? ForecastNumber { get; set; } 
 public virtual DateOnly? ForecastHorizonStart { get; set; } 
 public virtual DateOnly? ForecastHorizonEnd { get; set; } 
public virtual ICollection<ForecastLine> Lines { get; set; } = new List<ForecastLine>();
 public virtual ForecastMethod? Method { get; set; } 

    public static Forecast FromRequest(ForecastRequest request) {
        return new Forecast {
            Id = request.Id,
            ForecastNumber = request.ForecastNumber,
            ForecastHorizonStart = request.ForecastHorizonStart,
            ForecastHorizonEnd = request.ForecastHorizonEnd,
            Method = request.Method,
        };
    }
}
