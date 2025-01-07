using SMAIAXBackend.Domain.Model.ValueObjects;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;

namespace SMAIAXBackend.Application.Exceptions;

public class SmartMeterNotFoundException : Exception
{
    public SmartMeterNotFoundException(SmartMeterId smartMeterId)
        : base($"Smart meter with id '{smartMeterId.Id}' not found.")
    {
    }

    public SmartMeterNotFoundException(ConnectorSerialNumber serialNumber)
        : base($"Smart meter with serial number '{serialNumber.SerialNumber}' not found.")
    {
    }
}