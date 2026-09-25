
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class ForecastLineEndpoints
{
    public static IEndpointRouteBuilder MapForecastLineEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/forecastLine").WithTags("ForecastLines");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignForecast", AssignForecast);
        group.MapPut("/unassignForecast", UnassignForecast);
        group.MapPut("/assignItem", AssignItem);
        group.MapPut("/unassignItem", UnassignItem);


        return app;
    }

    private static async Task<IResult> Create(
        ForecastLineRequest request,
        IForecastLineService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToForecastLine( request );

        try
        {
            await service.Create(model, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.NoContent();
    }

    private static async Task<IResult> Update(
        ForecastLineRequest request,
        IForecastLineService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToForecastLine( request );

        try
        {
            var updated = await service.Update(model, cancellationToken);
            return updated ? Results.NoContent() : Results.NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }


    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IForecastLineService service,
        CancellationToken cancellationToken) {

        var forecastLine = await service.Get(identifier, cancellationToken);
        return forecastLine is null ? Results.NotFound() : Results.Ok( forecastLine );
    }


    private static async Task<IResult> GetAll(
        IForecastLineService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ForecastLineResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IForecastLineService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignForecast(
        AssociationRequest request,
        IForecastLineService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignForecast(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignForecast(
    AssociationRequest request,
    IForecastLineService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignForecast(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignItem(
        AssociationRequest request,
        IForecastLineService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignItem(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignItem(
    AssociationRequest request,
    IForecastLineService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignItem(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static ForecastLine mapRequestToForecastLine( ForecastLineRequest request ) {
        var model = new ForecastLine
        {
            Id = request.Id,
            Period = request.Period,
            Quantity = request.Quantity,
            Confidence = request.Confidence,
        };
        return model;
    }

}
