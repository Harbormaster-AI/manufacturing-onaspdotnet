
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class ShiftAssignmentEndpoints
{
    public static IEndpointRouteBuilder MapShiftAssignmentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/shiftAssignment").WithTags("ShiftAssignments");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignShift", AssignShift);
        group.MapPut("/unassignShift", UnassignShift);
        group.MapPut("/assignEmployee", AssignEmployee);
        group.MapPut("/unassignEmployee", UnassignEmployee);
        group.MapPut("/assignWorkCenter", AssignWorkCenter);
        group.MapPut("/unassignWorkCenter", UnassignWorkCenter);


        return app;
    }

    private static async Task<IResult> Create(
        ShiftAssignmentRequest request,
        IShiftAssignmentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToShiftAssignment( request );

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
        ShiftAssignmentRequest request,
        IShiftAssignmentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToShiftAssignment( request );

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
        IShiftAssignmentService service,
        CancellationToken cancellationToken) {

        var shiftAssignment = await service.Get(identifier, cancellationToken);
        return shiftAssignment is null ? Results.NotFound() : Results.Ok( shiftAssignment );
    }


    private static async Task<IResult> GetAll(
        IShiftAssignmentService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ShiftAssignmentResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IShiftAssignmentService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignShift(
        AssociationRequest request,
        IShiftAssignmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignShift(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignShift(
    AssociationRequest request,
    IShiftAssignmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignShift(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEmployee(
        AssociationRequest request,
        IShiftAssignmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEmployee(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEmployee(
    AssociationRequest request,
    IShiftAssignmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEmployee(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignWorkCenter(
        AssociationRequest request,
        IShiftAssignmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignWorkCenter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignWorkCenter(
    AssociationRequest request,
    IShiftAssignmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignWorkCenter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static ShiftAssignment mapRequestToShiftAssignment( ShiftAssignmentRequest request ) {
        var model = new ShiftAssignment
        {
            Id = request.Id,
            AssignmentDate = request.AssignmentDate,
        };
        return model;
    }

}
