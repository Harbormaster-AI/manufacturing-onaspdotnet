
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class PlannedOrderEndpoints
{
    public static IEndpointRouteBuilder MapPlannedOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/plannedOrder").WithTags("PlannedOrders");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignMrpRun", AssignMrpRun);
        group.MapPut("/unassignMrpRun", UnassignMrpRun);
        group.MapPut("/assignItem", AssignItem);
        group.MapPut("/unassignItem", UnassignItem);
        group.MapPut("/assignPlant", AssignPlant);
        group.MapPut("/unassignPlant", UnassignPlant);


        return app;
    }

    private static async Task<IResult> Create(
        PlannedOrderRequest request,
        IPlannedOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPlannedOrder( request );

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
        PlannedOrderRequest request,
        IPlannedOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPlannedOrder( request );

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
        IPlannedOrderService service,
        CancellationToken cancellationToken) {

        var plannedOrder = await service.Get(identifier, cancellationToken);
        return plannedOrder is null ? Results.NotFound() : Results.Ok( plannedOrder );
    }


    private static async Task<IResult> GetAll(
        IPlannedOrderService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PlannedOrderResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPlannedOrderService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignMrpRun(
        AssociationRequest request,
        IPlannedOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignMrpRun(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignMrpRun(
    AssociationRequest request,
    IPlannedOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignMrpRun(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignItem(
        AssociationRequest request,
        IPlannedOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignItem(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignItem(
    AssociationRequest request,
    IPlannedOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignItem(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPlant(
        AssociationRequest request,
        IPlannedOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPlant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPlant(
    AssociationRequest request,
    IPlannedOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPlant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static PlannedOrder mapRequestToPlannedOrder( PlannedOrderRequest request ) {
        var model = new PlannedOrder
        {
            Id = request.Id,
            PlannedOrderNumber = request.PlannedOrderNumber,
            Quantity = request.Quantity,
            DueDate = request.DueDate,
            OrderType = request.OrderType,
            Status = request.Status,
        };
        return model;
    }

}
