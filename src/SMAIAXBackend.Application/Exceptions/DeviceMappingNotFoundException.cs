namespace SMAIAXBackend.Application.Exceptions;

public class DeviceMappingNotFoundException(Guid serialNumber) : Exception
{
    public override string Message { get; } = $"No DeviceMapping found for serial number '{serialNumber}'.";
}