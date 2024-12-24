namespace SMAIAXBackend.Application.Exceptions;

public class DeviceConfigNotFoundException(Guid id) : Exception
{
    public override string Message { get; } = $"No Device Config found for Smart Meter with id '{id}'.";
}