
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class EmployeeEndpoints
{
    public static IEndpointRouteBuilder MapEmployeeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/employee").WithTags("Employees");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignWorkCenter", AssignWorkCenter);
        group.MapPut("/unassignWorkCenter", UnassignWorkCenter);

    group.MapPut("/addToShiftAssignments", AddToShiftAssignments);
    group.MapPut("/removeFromShiftAssignments", RemoveFromShiftAssignments);

    group.MapPut("/addToCorrectiveActions", AddToCorrectiveActions);
    group.MapPut("/removeFromCorrectiveActions", RemoveFromCorrectiveActions);


        return app;
    }

    private static async Task<IResult> Create(
        EmployeeRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToEmployee( request );

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
        EmployeeRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToEmployee( request );

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
        IEmployeeService service,
        CancellationToken cancellationToken) {

        var employee = await service.Get(identifier, cancellationToken);
        return employee is null ? Results.NotFound() : Results.Ok( employee );
    }


    private static async Task<IResult> GetAll(
        IEmployeeService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( EmployeeResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignWorkCenter(
        AssociationRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignWorkCenter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignWorkCenter(
    AssociationRequest request,
    IEmployeeService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignWorkCenter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToShiftAssignments(
        MultipleAssociationRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToShiftAssignments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromShiftAssignments(
        MultipleAssociationRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromShiftAssignments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCorrectiveActions(
        MultipleAssociationRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCorrectiveActions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCorrectiveActions(
        MultipleAssociationRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCorrectiveActions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Employee mapRequestToEmployee( EmployeeRequest request ) {
        var model = new Employee
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Role = request.Role,
            SkillLevel = request.SkillLevel,
        };
        return model;
    }

}
