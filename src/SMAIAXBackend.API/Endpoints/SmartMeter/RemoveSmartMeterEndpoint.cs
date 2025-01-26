using System.Security.Claims;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using SMAIAXBackend.Application.Services.Interfaces;

namespace SMAIAXBackend.API.Endpoints.SmartMeter;

public class RemoveSmartMeterEndpoint
{
    public static async Task<NoContent> Handle(
        ISmartMeterDeleteService smartMeterDeleteService,
        [FromRoute] Guid smartMeterId,
        ClaimsPrincipal user)
    {
        await smartMeterDeleteService.RemoveSmartMeterAsync(smartMeterId);

        return TypedResults.NoContent();
    }
    
}