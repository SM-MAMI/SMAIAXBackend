using SMAIAXBackend.Application.DTOs;

namespace SMAIAXBackend.API.Endpoints.Order;

public static class OrderEndpoints
{
    public static WebApplication MapOrderEndpoints(this WebApplication app)
    {
        const string contentType = "application/json";
        var group = app.MapGroup("/api/orders")
            .WithTags("Order")
            .RequireAuthorization();

        group.MapPost("/", OrderSmartMeterConnectorEndpoint.Handle)
            .WithName("OrderSmartMeterConnector")
            .Produces<SerialNumberDto>(StatusCodes.Status200OK, contentType)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        return app;
    }
}