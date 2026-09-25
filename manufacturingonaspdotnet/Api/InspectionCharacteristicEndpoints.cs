
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class InspectionCharacteristicEndpoints
{
    public static IEndpointRouteBuilder MapInspectionCharacteristicEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/inspectionCharacteristic").WithTags("InspectionCharacteristics");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignInspectionPlan", AssignInspectionPlan);
        group.MapPut("/unassignInspectionPlan", UnassignInspectionPlan);


        return app;
    }

    private static async Task<IResult> Create(
        InspectionCharacteristicRequest request,
        IInspectionCharacteristicService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInspectionCharacteristic( request );

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
        InspectionCharacteristicRequest request,
        IInspectionCharacteristicService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInspectionCharacteristic( request );

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
        IInspectionCharacteristicService service,
        CancellationToken cancellationToken) {

        var inspectionCharacteristic = await service.Get(identifier, cancellationToken);
        return inspectionCharacteristic is null ? Results.NotFound() : Results.Ok( inspectionCharacteristic );
    }


    private static async Task<IResult> GetAll(
        IInspectionCharacteristicService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( InspectionCharacteristicResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IInspectionCharacteristicService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignInspectionPlan(
        AssociationRequest request,
        IInspectionCharacteristicService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignInspectionPlan(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignInspectionPlan(
    AssociationRequest request,
    IInspectionCharacteristicService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignInspectionPlan(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static InspectionCharacteristic mapRequestToInspectionCharacteristic( InspectionCharacteristicRequest request ) {
        var model = new InspectionCharacteristic
        {
            Id = request.Id,
            CharacteristicCode = request.CharacteristicCode,
            Name = request.Name,
            LowerSpecLimit = request.LowerSpecLimit,
            UpperSpecLimit = request.UpperSpecLimit,
            Target = request.Target,
            MeasurementType = request.MeasurementType,
        };
        return model;
    }

}
