using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using SMAIAXBackend.Application.DTOs;
using SMAIAXBackend.Application.Services.Interfaces;
using SMAIAXBackend.Domain.Model.Enums;

namespace SMAIAXBackend.API.Endpoints.Policy;

public static class SearchPoliciesEndpoint
{
    public static async Task<Ok<List<PolicyDto>>> Handle(
        IPolicyListService policyListService,
        [FromQuery] decimal? maxPrice,
        [FromQuery] MeasurementResolution? measurementResolution)
    {
        var policies = await policyListService.GetFilteredPoliciesAsync(maxPrice, measurementResolution);

        return TypedResults.Ok(policies);
    }
}