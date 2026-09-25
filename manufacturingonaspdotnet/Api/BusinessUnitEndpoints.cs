
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class BusinessUnitEndpoints
{
    public static IEndpointRouteBuilder MapBusinessUnitEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/businessUnit").WithTags("BusinessUnits");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignEnterprise", AssignEnterprise);
        group.MapPut("/unassignEnterprise", UnassignEnterprise);

    group.MapPut("/addToItems", AddToItems);
    group.MapPut("/removeFromItems", RemoveFromItems);

    group.MapPut("/addToPlants", AddToPlants);
    group.MapPut("/removeFromPlants", RemoveFromPlants);


        return app;
    }

    private static async Task<IResult> Create(
        BusinessUnitRequest request,
        IBusinessUnitService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToBusinessUnit( request );

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
        BusinessUnitRequest request,
        IBusinessUnitService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToBusinessUnit( request );

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
        IBusinessUnitService service,
        CancellationToken cancellationToken) {

        var businessUnit = await service.Get(identifier, cancellationToken);
        return businessUnit is null ? Results.NotFound() : Results.Ok( businessUnit );
    }


    private static async Task<IResult> GetAll(
        IBusinessUnitService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( BusinessUnitResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IBusinessUnitService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEnterprise(
        AssociationRequest request,
        IBusinessUnitService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEnterprise(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEnterprise(
    AssociationRequest request,
    IBusinessUnitService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEnterprise(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToItems(
        MultipleAssociationRequest request,
        IBusinessUnitService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToItems(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromItems(
        MultipleAssociationRequest request,
        IBusinessUnitService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromItems(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPlants(
        MultipleAssociationRequest request,
        IBusinessUnitService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPlants(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPlants(
        MultipleAssociationRequest request,
        IBusinessUnitService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPlants(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static BusinessUnit mapRequestToBusinessUnit( BusinessUnitRequest request ) {
        var model = new BusinessUnit
        {
            Id = request.Id,
            Name = request.Name,
            Code = request.Code,
            Category = request.Category,
        };
        return model;
    }

}
