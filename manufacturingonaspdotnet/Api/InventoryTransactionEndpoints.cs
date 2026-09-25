
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class InventoryTransactionEndpoints
{
    public static IEndpointRouteBuilder MapInventoryTransactionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/inventoryTransaction").WithTags("InventoryTransactions");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignItem", AssignItem);
        group.MapPut("/unassignItem", UnassignItem);
        group.MapPut("/assignLocation", AssignLocation);
        group.MapPut("/unassignLocation", UnassignLocation);
        group.MapPut("/assignWorkOrder", AssignWorkOrder);
        group.MapPut("/unassignWorkOrder", UnassignWorkOrder);
        group.MapPut("/assignPurchaseOrder", AssignPurchaseOrder);
        group.MapPut("/unassignPurchaseOrder", UnassignPurchaseOrder);
        group.MapPut("/assignSalesOrder", AssignSalesOrder);
        group.MapPut("/unassignSalesOrder", UnassignSalesOrder);


        return app;
    }

    private static async Task<IResult> Create(
        InventoryTransactionRequest request,
        IInventoryTransactionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInventoryTransaction( request );

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
        InventoryTransactionRequest request,
        IInventoryTransactionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInventoryTransaction( request );

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
        IInventoryTransactionService service,
        CancellationToken cancellationToken) {

        var inventoryTransaction = await service.Get(identifier, cancellationToken);
        return inventoryTransaction is null ? Results.NotFound() : Results.Ok( inventoryTransaction );
    }


    private static async Task<IResult> GetAll(
        IInventoryTransactionService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( InventoryTransactionResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IInventoryTransactionService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignItem(
        AssociationRequest request,
        IInventoryTransactionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignItem(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignItem(
    AssociationRequest request,
    IInventoryTransactionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignItem(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignLocation(
        AssociationRequest request,
        IInventoryTransactionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignLocation(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignLocation(
    AssociationRequest request,
    IInventoryTransactionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignLocation(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignWorkOrder(
        AssociationRequest request,
        IInventoryTransactionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignWorkOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignWorkOrder(
    AssociationRequest request,
    IInventoryTransactionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignWorkOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPurchaseOrder(
        AssociationRequest request,
        IInventoryTransactionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPurchaseOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPurchaseOrder(
    AssociationRequest request,
    IInventoryTransactionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPurchaseOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSalesOrder(
        AssociationRequest request,
        IInventoryTransactionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignSalesOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignSalesOrder(
    AssociationRequest request,
    IInventoryTransactionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignSalesOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static InventoryTransaction mapRequestToInventoryTransaction( InventoryTransactionRequest request ) {
        var model = new InventoryTransaction
        {
            Id = request.Id,
            TransactionNumber = request.TransactionNumber,
            Quantity = request.Quantity,
            TransactionDateTime = request.TransactionDateTime,
            ReferenceDocument = request.ReferenceDocument,
            TransactionType = request.TransactionType,
        };
        return model;
    }

}
