
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class RoutingEndpoints
{
    public static IEndpointRouteBuilder MapRoutingEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/routing").WithTags("Routings");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignItem", AssignItem);
        group.MapPut("/unassignItem", UnassignItem);

    group.MapPut("/addToOperations", AddToOperations);
    group.MapPut("/removeFromOperations", RemoveFromOperations);


        return app;
    }

    private static async Task<IResult> Create(
        RoutingRequest request,
        IRoutingService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRouting( request );

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
        RoutingRequest request,
        IRoutingService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRouting( request );

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
        IRoutingService service,
        CancellationToken cancellationToken) {

        var routing = await service.Get(identifier, cancellationToken);
        return routing is null ? Results.NotFound() : Results.Ok( routing );
    }


    private static async Task<IResult> GetAll(
        IRoutingService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( RoutingResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IRoutingService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignItem(
        AssociationRequest request,
        IRoutingService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignItem(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignItem(
    AssociationRequest request,
    IRoutingService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignItem(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToOperations(
        MultipleAssociationRequest request,
        IRoutingService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToOperations(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromOperations(
        MultipleAssociationRequest request,
        IRoutingService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromOperations(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Routing mapRequestToRouting( RoutingRequest request ) {
        var model = new Routing
        {
            Id = request.Id,
            RoutingNumber = request.RoutingNumber,
            Revision = request.Revision,
            EffectivityStart = request.EffectivityStart,
            EffectivityEnd = request.EffectivityEnd,
            RoutingType = request.RoutingType,
            Status = request.Status,
        };
        return model;
    }

}
