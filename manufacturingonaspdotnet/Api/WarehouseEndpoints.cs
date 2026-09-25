
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class WarehouseEndpoints
{
    public static IEndpointRouteBuilder MapWarehouseEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/warehouse").WithTags("Warehouses");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPlant", AssignPlant);
        group.MapPut("/unassignPlant", UnassignPlant);

    group.MapPut("/addToLocations", AddToLocations);
    group.MapPut("/removeFromLocations", RemoveFromLocations);

    group.MapPut("/addToInventoryItems", AddToInventoryItems);
    group.MapPut("/removeFromInventoryItems", RemoveFromInventoryItems);


        return app;
    }

    private static async Task<IResult> Create(
        WarehouseRequest request,
        IWarehouseService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToWarehouse( request );

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
        WarehouseRequest request,
        IWarehouseService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToWarehouse( request );

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
        IWarehouseService service,
        CancellationToken cancellationToken) {

        var warehouse = await service.Get(identifier, cancellationToken);
        return warehouse is null ? Results.NotFound() : Results.Ok( warehouse );
    }


    private static async Task<IResult> GetAll(
        IWarehouseService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( WarehouseResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IWarehouseService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPlant(
        AssociationRequest request,
        IWarehouseService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPlant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPlant(
    AssociationRequest request,
    IWarehouseService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPlant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToLocations(
        MultipleAssociationRequest request,
        IWarehouseService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToLocations(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromLocations(
        MultipleAssociationRequest request,
        IWarehouseService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromLocations(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToInventoryItems(
        MultipleAssociationRequest request,
        IWarehouseService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToInventoryItems(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromInventoryItems(
        MultipleAssociationRequest request,
        IWarehouseService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromInventoryItems(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Warehouse mapRequestToWarehouse( WarehouseRequest request ) {
        var model = new Warehouse
        {
            Id = request.Id,
            Name = request.Name,
            WarehouseCode = request.WarehouseCode,
            Address = request.Address,
            WarehouseType = request.WarehouseType,
        };
        return model;
    }

}
