
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class CorrectiveActionEndpoints
{
    public static IEndpointRouteBuilder MapCorrectiveActionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/correctiveAction").WithTags("CorrectiveActions");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignNonconformance", AssignNonconformance);
        group.MapPut("/unassignNonconformance", UnassignNonconformance);
        group.MapPut("/assignOwner", AssignOwner);
        group.MapPut("/unassignOwner", UnassignOwner);


        return app;
    }

    private static async Task<IResult> Create(
        CorrectiveActionRequest request,
        ICorrectiveActionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCorrectiveAction( request );

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
        CorrectiveActionRequest request,
        ICorrectiveActionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCorrectiveAction( request );

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
        ICorrectiveActionService service,
        CancellationToken cancellationToken) {

        var correctiveAction = await service.Get(identifier, cancellationToken);
        return correctiveAction is null ? Results.NotFound() : Results.Ok( correctiveAction );
    }


    private static async Task<IResult> GetAll(
        ICorrectiveActionService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CorrectiveActionResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICorrectiveActionService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignNonconformance(
        AssociationRequest request,
        ICorrectiveActionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignNonconformance(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignNonconformance(
    AssociationRequest request,
    ICorrectiveActionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignNonconformance(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOwner(
        AssociationRequest request,
        ICorrectiveActionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOwner(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOwner(
    AssociationRequest request,
    ICorrectiveActionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOwner(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static CorrectiveAction mapRequestToCorrectiveAction( CorrectiveActionRequest request ) {
        var model = new CorrectiveAction
        {
            Id = request.Id,
            CapaNumber = request.CapaNumber,
            RootCause = request.RootCause,
            CorrectiveAction_ = request.CorrectiveAction_,
            VerificationDate = request.VerificationDate,
            Status = request.Status,
        };
        return model;
    }

}
