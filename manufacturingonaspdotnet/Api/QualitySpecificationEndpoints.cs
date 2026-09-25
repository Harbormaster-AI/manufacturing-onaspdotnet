
using manufacturingonaspdotnet.Service;
using manufacturingonaspdotnet.Domain;
using manufacturingonaspdotnet.Contracts;

namespace manufacturingonaspdotnet.Api;

public static class QualitySpecificationEndpoints
{
    public static IEndpointRouteBuilder MapQualitySpecificationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/qualitySpecification").WithTags("QualitySpecifications");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignItem", AssignItem);
        group.MapPut("/unassignItem", UnassignItem);


        return app;
    }

    private static async Task<IResult> Create(
        QualitySpecificationRequest request,
        IQualitySpecificationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToQualitySpecification( request );

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
        QualitySpecificationRequest request,
        IQualitySpecificationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToQualitySpecification( request );

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
        IQualitySpecificationService service,
        CancellationToken cancellationToken) {

        var qualitySpecification = await service.Get(identifier, cancellationToken);
        return qualitySpecification is null ? Results.NotFound() : Results.Ok( qualitySpecification );
    }


    private static async Task<IResult> GetAll(
        IQualitySpecificationService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( QualitySpecificationResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IQualitySpecificationService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignItem(
        AssociationRequest request,
        IQualitySpecificationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignItem(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignItem(
    AssociationRequest request,
    IQualitySpecificationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignItem(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static QualitySpecification mapRequestToQualitySpecification( QualitySpecificationRequest request ) {
        var model = new QualitySpecification
        {
            Id = request.Id,
            SpecCode = request.SpecCode,
            Name = request.Name,
            Version = request.Version,
        };
        return model;
    }

}
