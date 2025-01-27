using Microsoft.EntityFrameworkCore;

using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.ValueObjects;
using SMAIAXBackend.Domain.Repositories;
using SMAIAXBackend.Infrastructure.DbContexts;

namespace SMAIAXBackend.Infrastructure.Repositories;

public class DeviceMappingRepository(ApplicationDbContext applicationDbContext) : IDeviceMappingRepository
{
    public async Task AddAsync(DeviceMapping deviceMapping)
    {
        await applicationDbContext.DeviceMappings.AddAsync(deviceMapping);
        await applicationDbContext.SaveChangesAsync();
    }

    public async Task<DeviceMapping?> GetDeviceMappingBySerialNumberAsync(ConnectorSerialNumber serialNumber)
    {
        return await applicationDbContext.DeviceMappings.FirstOrDefaultAsync(dm => dm.ConnectorSerialNumber.Equals(serialNumber));
    }


    public async Task UpdateAsync(DeviceMapping deviceMapping)
    {
        applicationDbContext.DeviceMappings.Update(deviceMapping);
        await applicationDbContext.SaveChangesAsync();
    }
}