
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class PlantEndpoints
{
    public static IEndpointRouteBuilder MapPlantEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/plant").WithTags("Plants");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignEnterprise", AssignEnterprise);
        group.MapPut("/unassignEnterprise", UnassignEnterprise);

    group.MapPut("/addToProductionLines", AddToProductionLines);
    group.MapPut("/removeFromProductionLines", RemoveFromProductionLines);

    group.MapPut("/addToWorkCenters", AddToWorkCenters);
    group.MapPut("/removeFromWorkCenters", RemoveFromWorkCenters);

    group.MapPut("/addToWarehouses", AddToWarehouses);
    group.MapPut("/removeFromWarehouses", RemoveFromWarehouses);

    group.MapPut("/addToAssets", AddToAssets);
    group.MapPut("/removeFromAssets", RemoveFromAssets);

    group.MapPut("/addToProductionSchedules", AddToProductionSchedules);
    group.MapPut("/removeFromProductionSchedules", RemoveFromProductionSchedules);


        return app;
    }

    private static async Task<IResult> Create(
        PlantRequest request,
        IPlantService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPlant( request );

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
        PlantRequest request,
        IPlantService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPlant( request );

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
        IPlantService service,
        CancellationToken cancellationToken) {

        var plant = await service.Get(identifier, cancellationToken);
        return plant is null ? Results.NotFound() : Results.Ok( plant );
    }


    private static async Task<IResult> GetAll(
        IPlantService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PlantResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPlantService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEnterprise(
        AssociationRequest request,
        IPlantService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEnterprise(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEnterprise(
    AssociationRequest request,
    IPlantService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEnterprise(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToProductionLines(
        MultipleAssociationRequest request,
        IPlantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToProductionLines(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromProductionLines(
        MultipleAssociationRequest request,
        IPlantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromProductionLines(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToWorkCenters(
        MultipleAssociationRequest request,
        IPlantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToWorkCenters(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromWorkCenters(
        MultipleAssociationRequest request,
        IPlantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromWorkCenters(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToWarehouses(
        MultipleAssociationRequest request,
        IPlantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToWarehouses(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromWarehouses(
        MultipleAssociationRequest request,
        IPlantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromWarehouses(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToAssets(
        MultipleAssociationRequest request,
        IPlantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAssets(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAssets(
        MultipleAssociationRequest request,
        IPlantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAssets(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToProductionSchedules(
        MultipleAssociationRequest request,
        IPlantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToProductionSchedules(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromProductionSchedules(
        MultipleAssociationRequest request,
        IPlantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromProductionSchedules(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Plant mapRequestToPlant( PlantRequest request ) {
        var model = new Plant
        {
            Id = request.Id,
            Name = request.Name,
            PlantCode = request.PlantCode,
            Address = request.Address,
            TimeZone = request.TimeZone,
        };
        return model;
    }

}
