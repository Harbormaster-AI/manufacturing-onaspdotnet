
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class SalesOrderLineEndpoints
{
    public static IEndpointRouteBuilder MapSalesOrderLineEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/salesOrderLine").WithTags("SalesOrderLines");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignSalesOrder", AssignSalesOrder);
        group.MapPut("/unassignSalesOrder", UnassignSalesOrder);
        group.MapPut("/assignItem", AssignItem);
        group.MapPut("/unassignItem", UnassignItem);


        return app;
    }

    private static async Task<IResult> Create(
        SalesOrderLineRequest request,
        ISalesOrderLineService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSalesOrderLine( request );

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
        SalesOrderLineRequest request,
        ISalesOrderLineService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSalesOrderLine( request );

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
        ISalesOrderLineService service,
        CancellationToken cancellationToken) {

        var salesOrderLine = await service.Get(identifier, cancellationToken);
        return salesOrderLine is null ? Results.NotFound() : Results.Ok( salesOrderLine );
    }


    private static async Task<IResult> GetAll(
        ISalesOrderLineService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( SalesOrderLineResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ISalesOrderLineService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSalesOrder(
        AssociationRequest request,
        ISalesOrderLineService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignSalesOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignSalesOrder(
    AssociationRequest request,
    ISalesOrderLineService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignSalesOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignItem(
        AssociationRequest request,
        ISalesOrderLineService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignItem(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignItem(
    AssociationRequest request,
    ISalesOrderLineService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignItem(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static SalesOrderLine mapRequestToSalesOrderLine( SalesOrderLineRequest request ) {
        var model = new SalesOrderLine
        {
            Id = request.Id,
            LineNumber = request.LineNumber,
            Quantity = request.Quantity,
            UnitPrice = request.UnitPrice,
            DueDate = request.DueDate,
        };
        return model;
    }

}
