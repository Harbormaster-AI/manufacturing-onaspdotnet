
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class ProductionLineEndpoints
{
    public static IEndpointRouteBuilder MapProductionLineEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/productionLine").WithTags("ProductionLines");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPlant", AssignPlant);
        group.MapPut("/unassignPlant", UnassignPlant);

    group.MapPut("/addToWorkCenters", AddToWorkCenters);
    group.MapPut("/removeFromWorkCenters", RemoveFromWorkCenters);


        return app;
    }

    private static async Task<IResult> Create(
        ProductionLineRequest request,
        IProductionLineService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToProductionLine( request );

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
        ProductionLineRequest request,
        IProductionLineService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToProductionLine( request );

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
        IProductionLineService service,
        CancellationToken cancellationToken) {

        var productionLine = await service.Get(identifier, cancellationToken);
        return productionLine is null ? Results.NotFound() : Results.Ok( productionLine );
    }


    private static async Task<IResult> GetAll(
        IProductionLineService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ProductionLineResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IProductionLineService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPlant(
        AssociationRequest request,
        IProductionLineService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPlant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPlant(
    AssociationRequest request,
    IProductionLineService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPlant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToWorkCenters(
        MultipleAssociationRequest request,
        IProductionLineService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToWorkCenters(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromWorkCenters(
        MultipleAssociationRequest request,
        IProductionLineService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromWorkCenters(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static ProductionLine mapRequestToProductionLine( ProductionLineRequest request ) {
        var model = new ProductionLine
        {
            Id = request.Id,
            Name = request.Name,
            LineCode = request.LineCode,
            LineType = request.LineType,
        };
        return model;
    }

}
