
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class MaintenanceOrderEndpoints
{
    public static IEndpointRouteBuilder MapMaintenanceOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/maintenanceOrder").WithTags("MaintenanceOrders");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignAsset", AssignAsset);
        group.MapPut("/unassignAsset", UnassignAsset);
        group.MapPut("/assignPlan", AssignPlan);
        group.MapPut("/unassignPlan", UnassignPlan);
        group.MapPut("/assignWorkCenter", AssignWorkCenter);
        group.MapPut("/unassignWorkCenter", UnassignWorkCenter);


        return app;
    }

    private static async Task<IResult> Create(
        MaintenanceOrderRequest request,
        IMaintenanceOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMaintenanceOrder( request );

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
        MaintenanceOrderRequest request,
        IMaintenanceOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMaintenanceOrder( request );

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
        IMaintenanceOrderService service,
        CancellationToken cancellationToken) {

        var maintenanceOrder = await service.Get(identifier, cancellationToken);
        return maintenanceOrder is null ? Results.NotFound() : Results.Ok( maintenanceOrder );
    }


    private static async Task<IResult> GetAll(
        IMaintenanceOrderService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( MaintenanceOrderResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IMaintenanceOrderService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAsset(
        AssociationRequest request,
        IMaintenanceOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAsset(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAsset(
    AssociationRequest request,
    IMaintenanceOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAsset(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPlan(
        AssociationRequest request,
        IMaintenanceOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPlan(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPlan(
    AssociationRequest request,
    IMaintenanceOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPlan(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignWorkCenter(
        AssociationRequest request,
        IMaintenanceOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignWorkCenter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignWorkCenter(
    AssociationRequest request,
    IMaintenanceOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignWorkCenter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static MaintenanceOrder mapRequestToMaintenanceOrder( MaintenanceOrderRequest request ) {
        var model = new MaintenanceOrder
        {
            Id = request.Id,
            OrderNumber = request.OrderNumber,
            Priority = request.Priority,
            RequestedDate = request.RequestedDate,
            CompletionDate = request.CompletionDate,
            Status = request.Status,
        };
        return model;
    }

}
