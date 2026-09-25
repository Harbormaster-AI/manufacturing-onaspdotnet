
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class BOMItemEndpoints
{
    public static IEndpointRouteBuilder MapBOMItemEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/bOMItem").WithTags("BOMItems");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignBom", AssignBom);
        group.MapPut("/unassignBom", UnassignBom);
        group.MapPut("/assignComponent", AssignComponent);
        group.MapPut("/unassignComponent", UnassignComponent);


        return app;
    }

    private static async Task<IResult> Create(
        BOMItemRequest request,
        IBOMItemService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToBOMItem( request );

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
        BOMItemRequest request,
        IBOMItemService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToBOMItem( request );

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
        IBOMItemService service,
        CancellationToken cancellationToken) {

        var bOMItem = await service.Get(identifier, cancellationToken);
        return bOMItem is null ? Results.NotFound() : Results.Ok( bOMItem );
    }


    private static async Task<IResult> GetAll(
        IBOMItemService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( BOMItemResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IBOMItemService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignBom(
        AssociationRequest request,
        IBOMItemService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignBom(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignBom(
    AssociationRequest request,
    IBOMItemService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignBom(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignComponent(
        AssociationRequest request,
        IBOMItemService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignComponent(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignComponent(
    AssociationRequest request,
    IBOMItemService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignComponent(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static BOMItem mapRequestToBOMItem( BOMItemRequest request ) {
        var model = new BOMItem
        {
            Id = request.Id,
            LineNumber = request.LineNumber,
            Quantity = request.Quantity,
            ScrapPercent = request.ScrapPercent,
        };
        return model;
    }

}
