
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class WorkOrderEndpoints
{
    public static IEndpointRouteBuilder MapWorkOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/workOrder").WithTags("WorkOrders");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignItem", AssignItem);
        group.MapPut("/unassignItem", UnassignItem);
        group.MapPut("/assignPlant", AssignPlant);
        group.MapPut("/unassignPlant", UnassignPlant);
        group.MapPut("/assignRouting", AssignRouting);
        group.MapPut("/unassignRouting", UnassignRouting);
        group.MapPut("/assignBom", AssignBom);
        group.MapPut("/unassignBom", UnassignBom);
        group.MapPut("/assignProductionSchedule", AssignProductionSchedule);
        group.MapPut("/unassignProductionSchedule", UnassignProductionSchedule);
        group.MapPut("/assignSalesOrder", AssignSalesOrder);
        group.MapPut("/unassignSalesOrder", UnassignSalesOrder);


        return app;
    }

    private static async Task<IResult> Create(
        WorkOrderRequest request,
        IWorkOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToWorkOrder( request );

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
        WorkOrderRequest request,
        IWorkOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToWorkOrder( request );

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
        IWorkOrderService service,
        CancellationToken cancellationToken) {

        var workOrder = await service.Get(identifier, cancellationToken);
        return workOrder is null ? Results.NotFound() : Results.Ok( workOrder );
    }


    private static async Task<IResult> GetAll(
        IWorkOrderService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( WorkOrderResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IWorkOrderService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignItem(
        AssociationRequest request,
        IWorkOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignItem(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignItem(
    AssociationRequest request,
    IWorkOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignItem(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPlant(
        AssociationRequest request,
        IWorkOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPlant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPlant(
    AssociationRequest request,
    IWorkOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPlant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRouting(
        AssociationRequest request,
        IWorkOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignRouting(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignRouting(
    AssociationRequest request,
    IWorkOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignRouting(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignBom(
        AssociationRequest request,
        IWorkOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignBom(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignBom(
    AssociationRequest request,
    IWorkOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignBom(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignProductionSchedule(
        AssociationRequest request,
        IWorkOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignProductionSchedule(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignProductionSchedule(
    AssociationRequest request,
    IWorkOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignProductionSchedule(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSalesOrder(
        AssociationRequest request,
        IWorkOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignSalesOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignSalesOrder(
    AssociationRequest request,
    IWorkOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignSalesOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static WorkOrder mapRequestToWorkOrder( WorkOrderRequest request ) {
        var model = new WorkOrder
        {
            Id = request.Id,
            WorkOrderNumber = request.WorkOrderNumber,
            PlannedStart = request.PlannedStart,
            PlannedEnd = request.PlannedEnd,
            Quantity = request.Quantity,
            Priority = request.Priority,
            Status = request.Status,
        };
        return model;
    }

}
