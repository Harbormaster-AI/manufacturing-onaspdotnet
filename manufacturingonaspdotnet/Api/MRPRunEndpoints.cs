
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class MRPRunEndpoints
{
    public static IEndpointRouteBuilder MapMRPRunEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/mRPRun").WithTags("MRPRuns");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPlant", AssignPlant);
        group.MapPut("/unassignPlant", UnassignPlant);

    group.MapPut("/addToPlannedOrders", AddToPlannedOrders);
    group.MapPut("/removeFromPlannedOrders", RemoveFromPlannedOrders);


        return app;
    }

    private static async Task<IResult> Create(
        MRPRunRequest request,
        IMRPRunService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMRPRun( request );

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
        MRPRunRequest request,
        IMRPRunService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMRPRun( request );

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
        IMRPRunService service,
        CancellationToken cancellationToken) {

        var mRPRun = await service.Get(identifier, cancellationToken);
        return mRPRun is null ? Results.NotFound() : Results.Ok( mRPRun );
    }


    private static async Task<IResult> GetAll(
        IMRPRunService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( MRPRunResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IMRPRunService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPlant(
        AssociationRequest request,
        IMRPRunService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPlant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPlant(
    AssociationRequest request,
    IMRPRunService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPlant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToPlannedOrders(
        MultipleAssociationRequest request,
        IMRPRunService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPlannedOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPlannedOrders(
        MultipleAssociationRequest request,
        IMRPRunService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPlannedOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static MRPRun mapRequestToMRPRun( MRPRunRequest request ) {
        var model = new MRPRun
        {
            Id = request.Id,
            RunNumber = request.RunNumber,
            RunDateTime = request.RunDateTime,
            PlanningHorizonDays = request.PlanningHorizonDays,
            Status = request.Status,
        };
        return model;
    }

}
