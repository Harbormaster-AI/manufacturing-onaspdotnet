
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class AssetEndpoints
{
    public static IEndpointRouteBuilder MapAssetEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/asset").WithTags("Assets");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPlant", AssignPlant);
        group.MapPut("/unassignPlant", UnassignPlant);
        group.MapPut("/assignWorkCenter", AssignWorkCenter);
        group.MapPut("/unassignWorkCenter", UnassignWorkCenter);

    group.MapPut("/addToMaintenanceOrders", AddToMaintenanceOrders);
    group.MapPut("/removeFromMaintenanceOrders", RemoveFromMaintenanceOrders);

    group.MapPut("/addToMaintenancePlans", AddToMaintenancePlans);
    group.MapPut("/removeFromMaintenancePlans", RemoveFromMaintenancePlans);


        return app;
    }

    private static async Task<IResult> Create(
        AssetRequest request,
        IAssetService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAsset( request );

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
        AssetRequest request,
        IAssetService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAsset( request );

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
        IAssetService service,
        CancellationToken cancellationToken) {

        var asset = await service.Get(identifier, cancellationToken);
        return asset is null ? Results.NotFound() : Results.Ok( asset );
    }


    private static async Task<IResult> GetAll(
        IAssetService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AssetResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAssetService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPlant(
        AssociationRequest request,
        IAssetService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPlant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPlant(
    AssociationRequest request,
    IAssetService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPlant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignWorkCenter(
        AssociationRequest request,
        IAssetService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignWorkCenter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignWorkCenter(
    AssociationRequest request,
    IAssetService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignWorkCenter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToMaintenanceOrders(
        MultipleAssociationRequest request,
        IAssetService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToMaintenanceOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromMaintenanceOrders(
        MultipleAssociationRequest request,
        IAssetService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromMaintenanceOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToMaintenancePlans(
        MultipleAssociationRequest request,
        IAssetService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToMaintenancePlans(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromMaintenancePlans(
        MultipleAssociationRequest request,
        IAssetService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromMaintenancePlans(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Asset mapRequestToAsset( AssetRequest request ) {
        var model = new Asset
        {
            Id = request.Id,
            AssetTag = request.AssetTag,
            AssetName = request.AssetName,
            CommissioningDate = request.CommissioningDate,
            AssetStatus = request.AssetStatus,
        };
        return model;
    }

}
