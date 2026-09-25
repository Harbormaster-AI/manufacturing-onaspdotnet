
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class GoodsReceiptEndpoints
{
    public static IEndpointRouteBuilder MapGoodsReceiptEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/goodsReceipt").WithTags("GoodsReceipts");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPurchaseOrder", AssignPurchaseOrder);
        group.MapPut("/unassignPurchaseOrder", UnassignPurchaseOrder);
        group.MapPut("/assignWarehouse", AssignWarehouse);
        group.MapPut("/unassignWarehouse", UnassignWarehouse);

    group.MapPut("/addToLines", AddToLines);
    group.MapPut("/removeFromLines", RemoveFromLines);


        return app;
    }

    private static async Task<IResult> Create(
        GoodsReceiptRequest request,
        IGoodsReceiptService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToGoodsReceipt( request );

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
        GoodsReceiptRequest request,
        IGoodsReceiptService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToGoodsReceipt( request );

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
        IGoodsReceiptService service,
        CancellationToken cancellationToken) {

        var goodsReceipt = await service.Get(identifier, cancellationToken);
        return goodsReceipt is null ? Results.NotFound() : Results.Ok( goodsReceipt );
    }


    private static async Task<IResult> GetAll(
        IGoodsReceiptService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( GoodsReceiptResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IGoodsReceiptService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPurchaseOrder(
        AssociationRequest request,
        IGoodsReceiptService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPurchaseOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPurchaseOrder(
    AssociationRequest request,
    IGoodsReceiptService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPurchaseOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignWarehouse(
        AssociationRequest request,
        IGoodsReceiptService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignWarehouse(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignWarehouse(
    AssociationRequest request,
    IGoodsReceiptService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignWarehouse(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToLines(
        MultipleAssociationRequest request,
        IGoodsReceiptService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToLines(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromLines(
        MultipleAssociationRequest request,
        IGoodsReceiptService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromLines(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static GoodsReceipt mapRequestToGoodsReceipt( GoodsReceiptRequest request ) {
        var model = new GoodsReceipt
        {
            Id = request.Id,
            ReceiptNumber = request.ReceiptNumber,
            ReceiptDate = request.ReceiptDate,
            Status = request.Status,
        };
        return model;
    }

}
