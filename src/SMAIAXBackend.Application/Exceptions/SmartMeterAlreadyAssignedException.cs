using SMAIAXBackend.Domain.Model.ValueObjects;

namespace SMAIAXBackend.Application.Exceptions;

public class SmartMeterAlreadyAssignedException(ConnectorSerialNumber connectorSerialNumber) : Exception
{
    public override string Message { get; } = $"Smart Meter with serial number `{connectorSerialNumber}` is already assigned.";
}