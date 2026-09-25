
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class GoodsReceiptLineEndpoints
{
    public static IEndpointRouteBuilder MapGoodsReceiptLineEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/goodsReceiptLine").WithTags("GoodsReceiptLines");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignGoodsReceipt", AssignGoodsReceipt);
        group.MapPut("/unassignGoodsReceipt", UnassignGoodsReceipt);
        group.MapPut("/assignItem", AssignItem);
        group.MapPut("/unassignItem", UnassignItem);
        group.MapPut("/assignInventoryTransaction", AssignInventoryTransaction);
        group.MapPut("/unassignInventoryTransaction", UnassignInventoryTransaction);


        return app;
    }

    private static async Task<IResult> Create(
        GoodsReceiptLineRequest request,
        IGoodsReceiptLineService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToGoodsReceiptLine( request );

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
        GoodsReceiptLineRequest request,
        IGoodsReceiptLineService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToGoodsReceiptLine( request );

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
        IGoodsReceiptLineService service,
        CancellationToken cancellationToken) {

        var goodsReceiptLine = await service.Get(identifier, cancellationToken);
        return goodsReceiptLine is null ? Results.NotFound() : Results.Ok( goodsReceiptLine );
    }


    private static async Task<IResult> GetAll(
        IGoodsReceiptLineService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( GoodsReceiptLineResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IGoodsReceiptLineService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignGoodsReceipt(
        AssociationRequest request,
        IGoodsReceiptLineService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignGoodsReceipt(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignGoodsReceipt(
    AssociationRequest request,
    IGoodsReceiptLineService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignGoodsReceipt(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignItem(
        AssociationRequest request,
        IGoodsReceiptLineService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignItem(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignItem(
    AssociationRequest request,
    IGoodsReceiptLineService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignItem(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignInventoryTransaction(
        AssociationRequest request,
        IGoodsReceiptLineService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignInventoryTransaction(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignInventoryTransaction(
    AssociationRequest request,
    IGoodsReceiptLineService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignInventoryTransaction(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static GoodsReceiptLine mapRequestToGoodsReceiptLine( GoodsReceiptLineRequest request ) {
        var model = new GoodsReceiptLine
        {
            Id = request.Id,
            LineNumber = request.LineNumber,
            ReceivedQuantity = request.ReceivedQuantity,
            AcceptedQuantity = request.AcceptedQuantity,
            RejectedQuantity = request.RejectedQuantity,
            Lot = request.Lot,
        };
        return model;
    }

}
