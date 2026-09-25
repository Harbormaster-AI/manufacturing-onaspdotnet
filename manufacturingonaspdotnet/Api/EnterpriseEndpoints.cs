
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class EnterpriseEndpoints
{
    public static IEndpointRouteBuilder MapEnterpriseEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/enterprise").WithTags("Enterprises");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToBusinessUnits", AddToBusinessUnits);
    group.MapPut("/removeFromBusinessUnits", RemoveFromBusinessUnits);

    group.MapPut("/addToPlants", AddToPlants);
    group.MapPut("/removeFromPlants", RemoveFromPlants);

    group.MapPut("/addToSuppliers", AddToSuppliers);
    group.MapPut("/removeFromSuppliers", RemoveFromSuppliers);

    group.MapPut("/addToCustomers", AddToCustomers);
    group.MapPut("/removeFromCustomers", RemoveFromCustomers);


        return app;
    }

    private static async Task<IResult> Create(
        EnterpriseRequest request,
        IEnterpriseService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToEnterprise( request );

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
        EnterpriseRequest request,
        IEnterpriseService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToEnterprise( request );

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
        IEnterpriseService service,
        CancellationToken cancellationToken) {

        var enterprise = await service.Get(identifier, cancellationToken);
        return enterprise is null ? Results.NotFound() : Results.Ok( enterprise );
    }


    private static async Task<IResult> GetAll(
        IEnterpriseService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( EnterpriseResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IEnterpriseService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToBusinessUnits(
        MultipleAssociationRequest request,
        IEnterpriseService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToBusinessUnits(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromBusinessUnits(
        MultipleAssociationRequest request,
        IEnterpriseService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromBusinessUnits(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPlants(
        MultipleAssociationRequest request,
        IEnterpriseService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPlants(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPlants(
        MultipleAssociationRequest request,
        IEnterpriseService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPlants(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToSuppliers(
        MultipleAssociationRequest request,
        IEnterpriseService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToSuppliers(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromSuppliers(
        MultipleAssociationRequest request,
        IEnterpriseService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromSuppliers(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCustomers(
        MultipleAssociationRequest request,
        IEnterpriseService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCustomers(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCustomers(
        MultipleAssociationRequest request,
        IEnterpriseService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCustomers(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Enterprise mapRequestToEnterprise( EnterpriseRequest request ) {
        var model = new Enterprise
        {
            Id = request.Id,
            Name = request.Name,
            LegalName = request.LegalName,
            RegistrationCountry = request.RegistrationCountry,
            Website = request.Website,
            TaxId = request.TaxId,
        };
        return model;
    }

}
