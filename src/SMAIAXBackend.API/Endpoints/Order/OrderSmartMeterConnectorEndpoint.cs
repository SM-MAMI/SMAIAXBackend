using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using SMAIAXBackend.Application.Services.Interfaces;

namespace SMAIAXBackend.API.Endpoints.Order;

public static class OrderSmartMeterConnectorEndpoint
{
    public static async Task<Ok<Guid>> Handle(
        IOrderService orderService,
        IKeyGenerationService keyGenerationService)
    {
        var connectorSerialNumber = await orderService.OrderSmartMeterConnectorAsync();

        return TypedResults.Ok(connectorSerialNumber.Id);
    }
}