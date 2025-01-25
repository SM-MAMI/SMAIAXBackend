using SMAIAXBackend.Domain.Model.Entities;

namespace SMAIAXBackend.Domain.Repositories;

public interface IDeviceMappingRepository
{
    public Task AddAsync(DeviceMapping deviceMapping);
    public Task UpdateAsync(DeviceMapping deviceMapping);
}