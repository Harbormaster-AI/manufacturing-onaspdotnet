
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class ForecastEndpoints
{
    public static IEndpointRouteBuilder MapForecastEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/forecast").WithTags("Forecasts");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToLines", AddToLines);
    group.MapPut("/removeFromLines", RemoveFromLines);


        return app;
    }

    private static async Task<IResult> Create(
        ForecastRequest request,
        IForecastService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToForecast( request );

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
        ForecastRequest request,
        IForecastService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToForecast( request );

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
        IForecastService service,
        CancellationToken cancellationToken) {

        var forecast = await service.Get(identifier, cancellationToken);
        return forecast is null ? Results.NotFound() : Results.Ok( forecast );
    }


    private static async Task<IResult> GetAll(
        IForecastService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ForecastResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IForecastService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToLines(
        MultipleAssociationRequest request,
        IForecastService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToLines(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromLines(
        MultipleAssociationRequest request,
        IForecastService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromLines(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Forecast mapRequestToForecast( ForecastRequest request ) {
        var model = new Forecast
        {
            Id = request.Id,
            ForecastNumber = request.ForecastNumber,
            ForecastHorizonStart = request.ForecastHorizonStart,
            ForecastHorizonEnd = request.ForecastHorizonEnd,
            Method = request.Method,
        };
        return model;
    }

}
