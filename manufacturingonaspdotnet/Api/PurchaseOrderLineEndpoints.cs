
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class PurchaseOrderLineEndpoints
{
    public static IEndpointRouteBuilder MapPurchaseOrderLineEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/purchaseOrderLine").WithTags("PurchaseOrderLines");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPurchaseOrder", AssignPurchaseOrder);
        group.MapPut("/unassignPurchaseOrder", UnassignPurchaseOrder);
        group.MapPut("/assignItem", AssignItem);
        group.MapPut("/unassignItem", UnassignItem);


        return app;
    }

    private static async Task<IResult> Create(
        PurchaseOrderLineRequest request,
        IPurchaseOrderLineService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPurchaseOrderLine( request );

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
        PurchaseOrderLineRequest request,
        IPurchaseOrderLineService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPurchaseOrderLine( request );

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
        IPurchaseOrderLineService service,
        CancellationToken cancellationToken) {

        var purchaseOrderLine = await service.Get(identifier, cancellationToken);
        return purchaseOrderLine is null ? Results.NotFound() : Results.Ok( purchaseOrderLine );
    }


    private static async Task<IResult> GetAll(
        IPurchaseOrderLineService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PurchaseOrderLineResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPurchaseOrderLineService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPurchaseOrder(
        AssociationRequest request,
        IPurchaseOrderLineService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPurchaseOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPurchaseOrder(
    AssociationRequest request,
    IPurchaseOrderLineService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPurchaseOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignItem(
        AssociationRequest request,
        IPurchaseOrderLineService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignItem(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignItem(
    AssociationRequest request,
    IPurchaseOrderLineService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignItem(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static PurchaseOrderLine mapRequestToPurchaseOrderLine( PurchaseOrderLineRequest request ) {
        var model = new PurchaseOrderLine
        {
            Id = request.Id,
            LineNumber = request.LineNumber,
            Quantity = request.Quantity,
            UnitPrice = request.UnitPrice,
            DueDate = request.DueDate,
        };
        return model;
    }

}
