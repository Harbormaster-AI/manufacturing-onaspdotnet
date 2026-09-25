
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class OperationEndpoints
{
    public static IEndpointRouteBuilder MapOperationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/operation").WithTags("Operations");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignRouting", AssignRouting);
        group.MapPut("/unassignRouting", UnassignRouting);
        group.MapPut("/assignWorkCenter", AssignWorkCenter);
        group.MapPut("/unassignWorkCenter", UnassignWorkCenter);
        group.MapPut("/assignInspectionPlan", AssignInspectionPlan);
        group.MapPut("/unassignInspectionPlan", UnassignInspectionPlan);


        return app;
    }

    private static async Task<IResult> Create(
        OperationRequest request,
        IOperationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToOperation( request );

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
        OperationRequest request,
        IOperationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToOperation( request );

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
        IOperationService service,
        CancellationToken cancellationToken) {

        var operation = await service.Get(identifier, cancellationToken);
        return operation is null ? Results.NotFound() : Results.Ok( operation );
    }


    private static async Task<IResult> GetAll(
        IOperationService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( OperationResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IOperationService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRouting(
        AssociationRequest request,
        IOperationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignRouting(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignRouting(
    AssociationRequest request,
    IOperationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignRouting(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignWorkCenter(
        AssociationRequest request,
        IOperationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignWorkCenter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignWorkCenter(
    AssociationRequest request,
    IOperationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignWorkCenter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignInspectionPlan(
        AssociationRequest request,
        IOperationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignInspectionPlan(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignInspectionPlan(
    AssociationRequest request,
    IOperationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignInspectionPlan(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Operation mapRequestToOperation( OperationRequest request ) {
        var model = new Operation
        {
            Id = request.Id,
            OperationNumber = request.OperationNumber,
            Name = request.Name,
            SetupTime = request.SetupTime,
            StandardCycleTime = request.StandardCycleTime,
            OperationType = request.OperationType,
        };
        return model;
    }

}
