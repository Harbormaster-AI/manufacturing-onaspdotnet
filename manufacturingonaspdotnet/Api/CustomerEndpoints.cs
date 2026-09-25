
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class CustomerEndpoints
{
    public static IEndpointRouteBuilder MapCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/customer").WithTags("Customers");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToEnterprises", AddToEnterprises);
    group.MapPut("/removeFromEnterprises", RemoveFromEnterprises);

    group.MapPut("/addToSalesOrders", AddToSalesOrders);
    group.MapPut("/removeFromSalesOrders", RemoveFromSalesOrders);


        return app;
    }

    private static async Task<IResult> Create(
        CustomerRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCustomer( request );

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
        CustomerRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCustomer( request );

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
        ICustomerService service,
        CancellationToken cancellationToken) {

        var customer = await service.Get(identifier, cancellationToken);
        return customer is null ? Results.NotFound() : Results.Ok( customer );
    }


    private static async Task<IResult> GetAll(
        ICustomerService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CustomerResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToEnterprises(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToEnterprises(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromEnterprises(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromEnterprises(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToSalesOrders(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToSalesOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromSalesOrders(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromSalesOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Customer mapRequestToCustomer( CustomerRequest request ) {
        var model = new Customer
        {
            Id = request.Id,
            Name = request.Name,
            CustomerCode = request.CustomerCode,
            Address = request.Address,
            CustomerType = request.CustomerType,
        };
        return model;
    }

}
