namespace SMAIAXBackend.Domain.Model.ValueObjects;

public class ConnectorSerialNumber(Guid serialNumber) : ValueObject
{
    public Guid SerialNumber { get; private set; } = serialNumber;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return SerialNumber;
    }

    public override string ToString()
    {
        return SerialNumber.ToString();
    }
}