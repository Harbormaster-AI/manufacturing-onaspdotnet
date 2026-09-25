
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class PurchaseOrderEndpoints
{
    public static IEndpointRouteBuilder MapPurchaseOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/purchaseOrder").WithTags("PurchaseOrders");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignSupplier", AssignSupplier);
        group.MapPut("/unassignSupplier", UnassignSupplier);
        group.MapPut("/assignPlant", AssignPlant);
        group.MapPut("/unassignPlant", UnassignPlant);

    group.MapPut("/addToLines", AddToLines);
    group.MapPut("/removeFromLines", RemoveFromLines);

    group.MapPut("/addToGoodsReceipts", AddToGoodsReceipts);
    group.MapPut("/removeFromGoodsReceipts", RemoveFromGoodsReceipts);


        return app;
    }

    private static async Task<IResult> Create(
        PurchaseOrderRequest request,
        IPurchaseOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPurchaseOrder( request );

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
        PurchaseOrderRequest request,
        IPurchaseOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPurchaseOrder( request );

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
        IPurchaseOrderService service,
        CancellationToken cancellationToken) {

        var purchaseOrder = await service.Get(identifier, cancellationToken);
        return purchaseOrder is null ? Results.NotFound() : Results.Ok( purchaseOrder );
    }


    private static async Task<IResult> GetAll(
        IPurchaseOrderService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PurchaseOrderResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPurchaseOrderService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSupplier(
        AssociationRequest request,
        IPurchaseOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignSupplier(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignSupplier(
    AssociationRequest request,
    IPurchaseOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignSupplier(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPlant(
        AssociationRequest request,
        IPurchaseOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPlant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPlant(
    AssociationRequest request,
    IPurchaseOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPlant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToLines(
        MultipleAssociationRequest request,
        IPurchaseOrderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToLines(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromLines(
        MultipleAssociationRequest request,
        IPurchaseOrderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromLines(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToGoodsReceipts(
        MultipleAssociationRequest request,
        IPurchaseOrderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToGoodsReceipts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromGoodsReceipts(
        MultipleAssociationRequest request,
        IPurchaseOrderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromGoodsReceipts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static PurchaseOrder mapRequestToPurchaseOrder( PurchaseOrderRequest request ) {
        var model = new PurchaseOrder
        {
            Id = request.Id,
            PoNumber = request.PoNumber,
            OrderDate = request.OrderDate,
            TotalAmount = request.TotalAmount,
            Status = request.Status,
        };
        return model;
    }

}
