using SMAIAXBackend.Application.DTOs;

namespace SMAIAXBackend.API.Endpoints.Policy;

public static class PolicyEndpoints
{
    public static WebApplication MapPolicyEndpoints(this WebApplication app)
    {
        const string contentType = "application/json";
        var group = app.MapGroup("/api/policies")
            .WithTags("Policy")
            .RequireAuthorization();

        group.MapPost("/", CreatePolicyEndpoint.Handle)
            .WithName("createPolicy")
            .Accepts<PolicyCreateDto>(contentType)
            .Produces<Guid>(StatusCodes.Status200OK, contentType)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapGet("/", GetPoliciesEndpoint.Handle)
            .WithName("getPolicies")
            .Produces<List<PolicyDto>>(StatusCodes.Status200OK, contentType)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapGet("/search", SearchPoliciesEndpoint.Handle)
            .WithName("searchPolicies")
            .Produces<List<PolicyDto>>(StatusCodes.Status200OK, contentType)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapGet("/{id:guid}/measurements", GetMeasurementsByPolicyEndpoint.Handle)
            .WithName("getMeasurementsByPolicy")
            .Produces<MeasurementListDto>(StatusCodes.Status200OK, contentType)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        return app;
    }
}