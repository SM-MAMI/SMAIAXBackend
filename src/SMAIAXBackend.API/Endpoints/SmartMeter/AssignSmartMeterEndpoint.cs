using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using SMAIAXBackend.Application.DTOs;
using SMAIAXBackend.Application.Services.Interfaces;

namespace SMAIAXBackend.API.Endpoints.SmartMeter;

public static class AssignSmartMeterEndpoint
{
    public static async Task<Ok<Guid>> Handle(
        ISmartMeterCreateService smartMeterCreateService,
        [FromBody] SmartMeterAssignDto smartMeterAssignDto)
    {
        var id = await smartMeterCreateService.AssignSmartMeterAsync(smartMeterAssignDto);

        return TypedResults.Ok(id);
    }
}