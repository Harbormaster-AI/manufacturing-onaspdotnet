
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class ProductionScheduleEndpoints
{
    public static IEndpointRouteBuilder MapProductionScheduleEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/productionSchedule").WithTags("ProductionSchedules");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPlant", AssignPlant);
        group.MapPut("/unassignPlant", UnassignPlant);

    group.MapPut("/addToWorkOrders", AddToWorkOrders);
    group.MapPut("/removeFromWorkOrders", RemoveFromWorkOrders);


        return app;
    }

    private static async Task<IResult> Create(
        ProductionScheduleRequest request,
        IProductionScheduleService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToProductionSchedule( request );

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
        ProductionScheduleRequest request,
        IProductionScheduleService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToProductionSchedule( request );

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
        IProductionScheduleService service,
        CancellationToken cancellationToken) {

        var productionSchedule = await service.Get(identifier, cancellationToken);
        return productionSchedule is null ? Results.NotFound() : Results.Ok( productionSchedule );
    }


    private static async Task<IResult> GetAll(
        IProductionScheduleService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ProductionScheduleResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IProductionScheduleService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPlant(
        AssociationRequest request,
        IProductionScheduleService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPlant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPlant(
    AssociationRequest request,
    IProductionScheduleService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPlant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToWorkOrders(
        MultipleAssociationRequest request,
        IProductionScheduleService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToWorkOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromWorkOrders(
        MultipleAssociationRequest request,
        IProductionScheduleService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromWorkOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static ProductionSchedule mapRequestToProductionSchedule( ProductionScheduleRequest request ) {
        var model = new ProductionSchedule
        {
            Id = request.Id,
            ScheduleNumber = request.ScheduleNumber,
            HorizonStart = request.HorizonStart,
            HorizonEnd = request.HorizonEnd,
            Status = request.Status,
        };
        return model;
    }

}
