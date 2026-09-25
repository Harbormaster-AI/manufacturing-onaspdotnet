
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class InspectionResultEndpoints
{
    public static IEndpointRouteBuilder MapInspectionResultEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/inspectionResult").WithTags("InspectionResults");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignInspectionLot", AssignInspectionLot);
        group.MapPut("/unassignInspectionLot", UnassignInspectionLot);
        group.MapPut("/assignCharacteristic", AssignCharacteristic);
        group.MapPut("/unassignCharacteristic", UnassignCharacteristic);


        return app;
    }

    private static async Task<IResult> Create(
        InspectionResultRequest request,
        IInspectionResultService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInspectionResult( request );

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
        InspectionResultRequest request,
        IInspectionResultService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInspectionResult( request );

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
        IInspectionResultService service,
        CancellationToken cancellationToken) {

        var inspectionResult = await service.Get(identifier, cancellationToken);
        return inspectionResult is null ? Results.NotFound() : Results.Ok( inspectionResult );
    }


    private static async Task<IResult> GetAll(
        IInspectionResultService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( InspectionResultResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IInspectionResultService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignInspectionLot(
        AssociationRequest request,
        IInspectionResultService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignInspectionLot(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignInspectionLot(
    AssociationRequest request,
    IInspectionResultService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignInspectionLot(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCharacteristic(
        AssociationRequest request,
        IInspectionResultService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCharacteristic(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCharacteristic(
    AssociationRequest request,
    IInspectionResultService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCharacteristic(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static InspectionResult mapRequestToInspectionResult( InspectionResultRequest request ) {
        var model = new InspectionResult
        {
            Id = request.Id,
            ResultValue = request.ResultValue,
            RecordedOn = request.RecordedOn,
            Notes = request.Notes,
            ResultStatus = request.ResultStatus,
        };
        return model;
    }

}
