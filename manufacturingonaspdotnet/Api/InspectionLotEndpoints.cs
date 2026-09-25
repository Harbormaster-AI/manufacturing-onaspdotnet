
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class InspectionLotEndpoints
{
    public static IEndpointRouteBuilder MapInspectionLotEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/inspectionLot").WithTags("InspectionLots");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignItem", AssignItem);
        group.MapPut("/unassignItem", UnassignItem);
        group.MapPut("/assignWorkOrder", AssignWorkOrder);
        group.MapPut("/unassignWorkOrder", UnassignWorkOrder);
        group.MapPut("/assignGoodsReceipt", AssignGoodsReceipt);
        group.MapPut("/unassignGoodsReceipt", UnassignGoodsReceipt);

    group.MapPut("/addToResults", AddToResults);
    group.MapPut("/removeFromResults", RemoveFromResults);


        return app;
    }

    private static async Task<IResult> Create(
        InspectionLotRequest request,
        IInspectionLotService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInspectionLot( request );

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
        InspectionLotRequest request,
        IInspectionLotService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInspectionLot( request );

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
        IInspectionLotService service,
        CancellationToken cancellationToken) {

        var inspectionLot = await service.Get(identifier, cancellationToken);
        return inspectionLot is null ? Results.NotFound() : Results.Ok( inspectionLot );
    }


    private static async Task<IResult> GetAll(
        IInspectionLotService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( InspectionLotResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IInspectionLotService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignItem(
        AssociationRequest request,
        IInspectionLotService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignItem(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignItem(
    AssociationRequest request,
    IInspectionLotService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignItem(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignWorkOrder(
        AssociationRequest request,
        IInspectionLotService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignWorkOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignWorkOrder(
    AssociationRequest request,
    IInspectionLotService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignWorkOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignGoodsReceipt(
        AssociationRequest request,
        IInspectionLotService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignGoodsReceipt(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignGoodsReceipt(
    AssociationRequest request,
    IInspectionLotService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignGoodsReceipt(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToResults(
        MultipleAssociationRequest request,
        IInspectionLotService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToResults(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromResults(
        MultipleAssociationRequest request,
        IInspectionLotService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromResults(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static InspectionLot mapRequestToInspectionLot( InspectionLotRequest request ) {
        var model = new InspectionLot
        {
            Id = request.Id,
            LotNumber = request.LotNumber,
            Quantity = request.Quantity,
            SampleSize = request.SampleSize,
            CreatedOn = request.CreatedOn,
            InspectionType = request.InspectionType,
            Status = request.Status,
        };
        return model;
    }

}
