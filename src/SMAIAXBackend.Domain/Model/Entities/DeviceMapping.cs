using SMAIAXBackend.Domain.Model.ValueObjects;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;

namespace SMAIAXBackend.Domain.Model.Entities;

public class DeviceMapping
{
    public ConnectorSerialNumber ConnectorSerialNumber { get; }
    public string PublicKey { get; }
    public UserId? AssignedUser { get; private set; }
    
    public static DeviceMapping Create(
        ConnectorSerialNumber connectorSerialNumber,
        string publicKey,
        UserId? assignedUser)
    {
        return new DeviceMapping(connectorSerialNumber, publicKey, assignedUser);
    }
    
    // Needed by EF Core
    private DeviceMapping()
    {
    }
    
    private DeviceMapping(
        ConnectorSerialNumber connectorSerialNumber,
        string publicKey,
        UserId? assignedUser)
    {
        ConnectorSerialNumber = connectorSerialNumber;
        PublicKey = publicKey;
        AssignedUser = assignedUser;
    }
    
    public void DeleteAssignment()
    {
        AssignedUser = null;
    }
}