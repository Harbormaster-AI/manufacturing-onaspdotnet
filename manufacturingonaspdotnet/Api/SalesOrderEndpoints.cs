
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class SalesOrderEndpoints
{
    public static IEndpointRouteBuilder MapSalesOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/salesOrder").WithTags("SalesOrders");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCustomer", AssignCustomer);
        group.MapPut("/unassignCustomer", UnassignCustomer);
        group.MapPut("/assignPlant", AssignPlant);
        group.MapPut("/unassignPlant", UnassignPlant);

    group.MapPut("/addToLines", AddToLines);
    group.MapPut("/removeFromLines", RemoveFromLines);

    group.MapPut("/addToWorkOrders", AddToWorkOrders);
    group.MapPut("/removeFromWorkOrders", RemoveFromWorkOrders);


        return app;
    }

    private static async Task<IResult> Create(
        SalesOrderRequest request,
        ISalesOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSalesOrder( request );

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
        SalesOrderRequest request,
        ISalesOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSalesOrder( request );

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
        ISalesOrderService service,
        CancellationToken cancellationToken) {

        var salesOrder = await service.Get(identifier, cancellationToken);
        return salesOrder is null ? Results.NotFound() : Results.Ok( salesOrder );
    }


    private static async Task<IResult> GetAll(
        ISalesOrderService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( SalesOrderResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ISalesOrderService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCustomer(
        AssociationRequest request,
        ISalesOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCustomer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCustomer(
    AssociationRequest request,
    ISalesOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCustomer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPlant(
        AssociationRequest request,
        ISalesOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPlant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPlant(
    AssociationRequest request,
    ISalesOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPlant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToLines(
        MultipleAssociationRequest request,
        ISalesOrderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToLines(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromLines(
        MultipleAssociationRequest request,
        ISalesOrderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromLines(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToWorkOrders(
        MultipleAssociationRequest request,
        ISalesOrderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToWorkOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromWorkOrders(
        MultipleAssociationRequest request,
        ISalesOrderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromWorkOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static SalesOrder mapRequestToSalesOrder( SalesOrderRequest request ) {
        var model = new SalesOrder
        {
            Id = request.Id,
            OrderNumber = request.OrderNumber,
            OrderDate = request.OrderDate,
            TotalAmount = request.TotalAmount,
            Status = request.Status,
        };
        return model;
    }

}
