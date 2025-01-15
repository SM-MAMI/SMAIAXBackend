using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.ValueObjects;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;

namespace SMAIAXBackend.Domain.Repositories;

public interface ISmartMeterRepository
{
    SmartMeterId NextIdentity();
    MetadataId NextMetadataIdentity();
    Task AddAsync(SmartMeter meter);
    Task<List<SmartMeter>> GetSmartMetersAsync();
    Task<SmartMeter?> GetSmartMeterByIdAsync(SmartMeterId smartMeterId);
    Task<SmartMeter?> GetSmartMeterBySerialNumberAsync(ConnectorSerialNumber serialNumber);
    Task UpdateAsync(SmartMeter smartMeter);
    Task<SmartMeter?> GetSmartMeterByTenantAndIdAsync(Tenant tenant, SmartMeterId smartMeterId);
}