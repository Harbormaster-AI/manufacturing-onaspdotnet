
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class ItemEndpoints
{
    public static IEndpointRouteBuilder MapItemEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/item").WithTags("Items");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignBusinessUnit", AssignBusinessUnit);
        group.MapPut("/unassignBusinessUnit", UnassignBusinessUnit);

    group.MapPut("/addToBoms", AddToBoms);
    group.MapPut("/removeFromBoms", RemoveFromBoms);

    group.MapPut("/addToRoutings", AddToRoutings);
    group.MapPut("/removeFromRoutings", RemoveFromRoutings);

    group.MapPut("/addToSuppliers", AddToSuppliers);
    group.MapPut("/removeFromSuppliers", RemoveFromSuppliers);

    group.MapPut("/addToQualitySpecifications", AddToQualitySpecifications);
    group.MapPut("/removeFromQualitySpecifications", RemoveFromQualitySpecifications);

    group.MapPut("/addToInventoryItems", AddToInventoryItems);
    group.MapPut("/removeFromInventoryItems", RemoveFromInventoryItems);


        return app;
    }

    private static async Task<IResult> Create(
        ItemRequest request,
        IItemService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToItem( request );

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
        ItemRequest request,
        IItemService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToItem( request );

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
        IItemService service,
        CancellationToken cancellationToken) {

        var item = await service.Get(identifier, cancellationToken);
        return item is null ? Results.NotFound() : Results.Ok( item );
    }


    private static async Task<IResult> GetAll(
        IItemService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ItemResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IItemService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignBusinessUnit(
        AssociationRequest request,
        IItemService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignBusinessUnit(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignBusinessUnit(
    AssociationRequest request,
    IItemService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignBusinessUnit(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToBoms(
        MultipleAssociationRequest request,
        IItemService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToBoms(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromBoms(
        MultipleAssociationRequest request,
        IItemService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromBoms(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToRoutings(
        MultipleAssociationRequest request,
        IItemService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRoutings(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRoutings(
        MultipleAssociationRequest request,
        IItemService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRoutings(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToSuppliers(
        MultipleAssociationRequest request,
        IItemService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToSuppliers(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromSuppliers(
        MultipleAssociationRequest request,
        IItemService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromSuppliers(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToQualitySpecifications(
        MultipleAssociationRequest request,
        IItemService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToQualitySpecifications(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromQualitySpecifications(
        MultipleAssociationRequest request,
        IItemService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromQualitySpecifications(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToInventoryItems(
        MultipleAssociationRequest request,
        IItemService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToInventoryItems(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromInventoryItems(
        MultipleAssociationRequest request,
        IItemService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromInventoryItems(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Item mapRequestToItem( ItemRequest request ) {
        var model = new Item
        {
            Id = request.Id,
            ItemNumber = request.ItemNumber,
            Name = request.Name,
            StandardCost = request.StandardCost,
            Weight = request.Weight,
            AsSerialControlled = request.AsSerialControlled,
            ItemType = request.ItemType,
            ProcurementType = request.ProcurementType,
            UnitOfMeasure = request.UnitOfMeasure,
            LifecycleStatus = request.LifecycleStatus,
        };
        return model;
    }

}
