using SMAIAXBackend.Domain.Model.ValueObjects;

namespace SMAIAXBackend.Application.Services.Interfaces;

public interface IOrderService
{
    Task<ConnectorSerialNumber> OrderSmartMeterConnectorAsync();
}