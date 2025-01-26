using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.ValueObjects;

namespace SMAIAXBackend.Domain.Repositories;

public interface IDeviceMappingRepository
{
    public Task AddAsync(DeviceMapping deviceMapping);
    public Task<DeviceMapping?> GetDeviceMappingBySerialNumberAsync(ConnectorSerialNumber serialNumber);
    public Task UpdateAsync(DeviceMapping deviceMapping);
}