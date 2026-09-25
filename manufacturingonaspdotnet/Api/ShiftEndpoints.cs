
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class ShiftEndpoints
{
    public static IEndpointRouteBuilder MapShiftEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/shift").WithTags("Shifts");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPlant", AssignPlant);
        group.MapPut("/unassignPlant", UnassignPlant);

    group.MapPut("/addToAssignments", AddToAssignments);
    group.MapPut("/removeFromAssignments", RemoveFromAssignments);


        return app;
    }

    private static async Task<IResult> Create(
        ShiftRequest request,
        IShiftService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToShift( request );

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
        ShiftRequest request,
        IShiftService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToShift( request );

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
        IShiftService service,
        CancellationToken cancellationToken) {

        var shift = await service.Get(identifier, cancellationToken);
        return shift is null ? Results.NotFound() : Results.Ok( shift );
    }


    private static async Task<IResult> GetAll(
        IShiftService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ShiftResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IShiftService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPlant(
        AssociationRequest request,
        IShiftService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPlant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPlant(
    AssociationRequest request,
    IShiftService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPlant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToAssignments(
        MultipleAssociationRequest request,
        IShiftService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAssignments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAssignments(
        MultipleAssociationRequest request,
        IShiftService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAssignments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Shift mapRequestToShift( ShiftRequest request ) {
        var model = new Shift
        {
            Id = request.Id,
            ShiftName = request.ShiftName,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            ShiftType = request.ShiftType,
        };
        return model;
    }

}
