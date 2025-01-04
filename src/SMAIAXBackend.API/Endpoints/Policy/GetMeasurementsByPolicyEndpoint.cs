using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using SMAIAXBackend.Application.DTOs;
using SMAIAXBackend.Application.Services.Interfaces;

namespace SMAIAXBackend.API.Endpoints.Policy;

public static class GetMeasurementsByPolicyEndpoint
{
    public static async Task<Ok<List<MeasurementDto>>> Handle(
        IPolicyListService policyListService,
        [FromRoute] Guid id)
    {
        var measurements = await policyListService.GetMeasurementsByPolicyIdAsync(id);
        return TypedResults.Ok(measurements);
    }
}