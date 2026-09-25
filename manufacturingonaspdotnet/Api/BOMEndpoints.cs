
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class BOMEndpoints
{
    public static IEndpointRouteBuilder MapBOMEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/bOM").WithTags("BOMs");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignParentItem", AssignParentItem);
        group.MapPut("/unassignParentItem", UnassignParentItem);

    group.MapPut("/addToBomItems", AddToBomItems);
    group.MapPut("/removeFromBomItems", RemoveFromBomItems);


        return app;
    }

    private static async Task<IResult> Create(
        BOMRequest request,
        IBOMService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToBOM( request );

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
        BOMRequest request,
        IBOMService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToBOM( request );

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
        IBOMService service,
        CancellationToken cancellationToken) {

        var bOM = await service.Get(identifier, cancellationToken);
        return bOM is null ? Results.NotFound() : Results.Ok( bOM );
    }


    private static async Task<IResult> GetAll(
        IBOMService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( BOMResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IBOMService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignParentItem(
        AssociationRequest request,
        IBOMService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignParentItem(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignParentItem(
    AssociationRequest request,
    IBOMService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignParentItem(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToBomItems(
        MultipleAssociationRequest request,
        IBOMService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToBomItems(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromBomItems(
        MultipleAssociationRequest request,
        IBOMService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromBomItems(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static BOM mapRequestToBOM( BOMRequest request ) {
        var model = new BOM
        {
            Id = request.Id,
            BomNumber = request.BomNumber,
            Revision = request.Revision,
            EffectivityStart = request.EffectivityStart,
            EffectivityEnd = request.EffectivityEnd,
            Status = request.Status,
        };
        return model;
    }

}
