using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using SMAIAXBackend.Application.DTOs;
using SMAIAXBackend.Application.Services.Interfaces;

namespace SMAIAXBackend.API.Endpoints.Order;

public static class OrderSmartMeterConnectorEndpoint
{
    public static async Task<Ok<SerialNumberDto>> Handle(
        IOrderService orderService)
    {
        var connectorSerialNumber = await orderService.OrderSmartMeterConnectorAsync();

        return TypedResults.Ok(new SerialNumberDto(connectorSerialNumber.SerialNumber));
    }
}