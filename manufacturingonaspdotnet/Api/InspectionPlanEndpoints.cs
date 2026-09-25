
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class InspectionPlanEndpoints
{
    public static IEndpointRouteBuilder MapInspectionPlanEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/inspectionPlan").WithTags("InspectionPlans");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignItem", AssignItem);
        group.MapPut("/unassignItem", UnassignItem);

    group.MapPut("/addToCharacteristics", AddToCharacteristics);
    group.MapPut("/removeFromCharacteristics", RemoveFromCharacteristics);


        return app;
    }

    private static async Task<IResult> Create(
        InspectionPlanRequest request,
        IInspectionPlanService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInspectionPlan( request );

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
        InspectionPlanRequest request,
        IInspectionPlanService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInspectionPlan( request );

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
        IInspectionPlanService service,
        CancellationToken cancellationToken) {

        var inspectionPlan = await service.Get(identifier, cancellationToken);
        return inspectionPlan is null ? Results.NotFound() : Results.Ok( inspectionPlan );
    }


    private static async Task<IResult> GetAll(
        IInspectionPlanService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( InspectionPlanResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IInspectionPlanService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignItem(
        AssociationRequest request,
        IInspectionPlanService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignItem(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignItem(
    AssociationRequest request,
    IInspectionPlanService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignItem(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToCharacteristics(
        MultipleAssociationRequest request,
        IInspectionPlanService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCharacteristics(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCharacteristics(
        MultipleAssociationRequest request,
        IInspectionPlanService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCharacteristics(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static InspectionPlan mapRequestToInspectionPlan( InspectionPlanRequest request ) {
        var model = new InspectionPlan
        {
            Id = request.Id,
            PlanNumber = request.PlanNumber,
            Revision = request.Revision,
            SamplingPlan = request.SamplingPlan,
            Status = request.Status,
        };
        return model;
    }

}
