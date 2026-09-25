
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class NonconformanceEndpoints
{
    public static IEndpointRouteBuilder MapNonconformanceEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/nonconformance").WithTags("Nonconformances");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignItem", AssignItem);
        group.MapPut("/unassignItem", UnassignItem);
        group.MapPut("/assignWorkOrder", AssignWorkOrder);
        group.MapPut("/unassignWorkOrder", UnassignWorkOrder);
        group.MapPut("/assignInspectionLot", AssignInspectionLot);
        group.MapPut("/unassignInspectionLot", UnassignInspectionLot);
        group.MapPut("/assignCorrectiveAction", AssignCorrectiveAction);
        group.MapPut("/unassignCorrectiveAction", UnassignCorrectiveAction);


        return app;
    }

    private static async Task<IResult> Create(
        NonconformanceRequest request,
        INonconformanceService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToNonconformance( request );

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
        NonconformanceRequest request,
        INonconformanceService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToNonconformance( request );

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
        INonconformanceService service,
        CancellationToken cancellationToken) {

        var nonconformance = await service.Get(identifier, cancellationToken);
        return nonconformance is null ? Results.NotFound() : Results.Ok( nonconformance );
    }


    private static async Task<IResult> GetAll(
        INonconformanceService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( NonconformanceResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        INonconformanceService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignItem(
        AssociationRequest request,
        INonconformanceService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignItem(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignItem(
    AssociationRequest request,
    INonconformanceService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignItem(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignWorkOrder(
        AssociationRequest request,
        INonconformanceService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignWorkOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignWorkOrder(
    AssociationRequest request,
    INonconformanceService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignWorkOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignInspectionLot(
        AssociationRequest request,
        INonconformanceService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignInspectionLot(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignInspectionLot(
    AssociationRequest request,
    INonconformanceService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignInspectionLot(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCorrectiveAction(
        AssociationRequest request,
        INonconformanceService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCorrectiveAction(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCorrectiveAction(
    AssociationRequest request,
    INonconformanceService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCorrectiveAction(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Nonconformance mapRequestToNonconformance( NonconformanceRequest request ) {
        var model = new Nonconformance
        {
            Id = request.Id,
            NcNumber = request.NcNumber,
            Description = request.Description,
            ContainmentAction = request.ContainmentAction,
            NcType = request.NcType,
            Severity = request.Severity,
            Status = request.Status,
        };
        return model;
    }

}
