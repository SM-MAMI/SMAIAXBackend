using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using SMAIAXBackend.Application.DTOs;
using SMAIAXBackend.Application.Services.Interfaces;
using SMAIAXBackend.Domain.Model.Enums;

namespace SMAIAXBackend.API.Endpoints.Contract;

public static class GetMeasurementsByContractEndpoint
{
    public static async Task<Ok<MeasurementListDto>> Handle(IContractListService contractListService,
        [FromRoute] Guid id, [FromQuery] MeasurementResolution? measurementResolution, [FromQuery] DateTime? startAt,
        [FromQuery] DateTime? endAt)
    {
        var measurements =
            await contractListService.GetMeasurementsByContractIdAsync(id, measurementResolution, startAt, endAt);

        return TypedResults.Ok(measurements);
    }
}