
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class MaintenancePlanEndpoints
{
    public static IEndpointRouteBuilder MapMaintenancePlanEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/maintenancePlan").WithTags("MaintenancePlans");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignAsset", AssignAsset);
        group.MapPut("/unassignAsset", UnassignAsset);

    group.MapPut("/addToMaintenanceOrders", AddToMaintenanceOrders);
    group.MapPut("/removeFromMaintenanceOrders", RemoveFromMaintenanceOrders);


        return app;
    }

    private static async Task<IResult> Create(
        MaintenancePlanRequest request,
        IMaintenancePlanService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMaintenancePlan( request );

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
        MaintenancePlanRequest request,
        IMaintenancePlanService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMaintenancePlan( request );

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
        IMaintenancePlanService service,
        CancellationToken cancellationToken) {

        var maintenancePlan = await service.Get(identifier, cancellationToken);
        return maintenancePlan is null ? Results.NotFound() : Results.Ok( maintenancePlan );
    }


    private static async Task<IResult> GetAll(
        IMaintenancePlanService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( MaintenancePlanResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IMaintenancePlanService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAsset(
        AssociationRequest request,
        IMaintenancePlanService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAsset(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAsset(
    AssociationRequest request,
    IMaintenancePlanService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAsset(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToMaintenanceOrders(
        MultipleAssociationRequest request,
        IMaintenancePlanService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToMaintenanceOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromMaintenanceOrders(
        MultipleAssociationRequest request,
        IMaintenancePlanService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromMaintenanceOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static MaintenancePlan mapRequestToMaintenancePlan( MaintenancePlanRequest request ) {
        var model = new MaintenancePlan
        {
            Id = request.Id,
            PlanNumber = request.PlanNumber,
            Interval = request.Interval,
            LastServiceDate = request.LastServiceDate,
            Strategy = request.Strategy,
        };
        return model;
    }

}
