
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class SupplierEndpoints
{
    public static IEndpointRouteBuilder MapSupplierEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/supplier").WithTags("Suppliers");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToEnterprises", AddToEnterprises);
    group.MapPut("/removeFromEnterprises", RemoveFromEnterprises);

    group.MapPut("/addToItems", AddToItems);
    group.MapPut("/removeFromItems", RemoveFromItems);

    group.MapPut("/addToPurchaseOrders", AddToPurchaseOrders);
    group.MapPut("/removeFromPurchaseOrders", RemoveFromPurchaseOrders);


        return app;
    }

    private static async Task<IResult> Create(
        SupplierRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSupplier( request );

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
        SupplierRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSupplier( request );

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
        ISupplierService service,
        CancellationToken cancellationToken) {

        var supplier = await service.Get(identifier, cancellationToken);
        return supplier is null ? Results.NotFound() : Results.Ok( supplier );
    }


    private static async Task<IResult> GetAll(
        ISupplierService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( SupplierResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ISupplierService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToEnterprises(
        MultipleAssociationRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToEnterprises(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromEnterprises(
        MultipleAssociationRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromEnterprises(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToItems(
        MultipleAssociationRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToItems(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromItems(
        MultipleAssociationRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromItems(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPurchaseOrders(
        MultipleAssociationRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPurchaseOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPurchaseOrders(
        MultipleAssociationRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPurchaseOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Supplier mapRequestToSupplier( SupplierRequest request ) {
        var model = new Supplier
        {
            Id = request.Id,
            Name = request.Name,
            SupplierCode = request.SupplierCode,
            Address = request.Address,
            SupplierTier = request.SupplierTier,
            PaymentTerms = request.PaymentTerms,
        };
        return model;
    }

}
