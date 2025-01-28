using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using SMAIAXBackend.Application.DTOs;
using SMAIAXBackend.Application.Services.Interfaces;
using SMAIAXBackend.Domain.Model.Enums;

namespace SMAIAXBackend.API.Endpoints.Policy;

public static class GetMeasurementsByPolicyEndpoint
{
    public static async Task<Ok<MeasurementListDto>> Handle(
        IPolicyListService policyListService,
        [FromRoute] Guid id,[FromQuery] MeasurementResolution? measurementResolution, [FromQuery] DateTime? startAt, [FromQuery] DateTime? endAt)
    {
        var measurementList = await policyListService.GetMeasurementsByPolicyIdAsync(id,measurementResolution, startAt, endAt);
        return TypedResults.Ok(measurementList);
    }
}