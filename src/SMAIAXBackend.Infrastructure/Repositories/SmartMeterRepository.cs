using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.ValueObjects;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;
using SMAIAXBackend.Domain.Repositories;
using SMAIAXBackend.Infrastructure.Configurations;
using SMAIAXBackend.Infrastructure.DbContexts;

namespace SMAIAXBackend.Infrastructure.Repositories;

public class SmartMeterRepository(
    TenantDbContext tenantDbContext,
    ITenantDbContextFactory tenantDbContextFactory,
    IOptions<DatabaseConfiguration> databaseConfigOptions) : ISmartMeterRepository
{
    public SmartMeterId NextIdentity()
    {
        return new SmartMeterId(Guid.NewGuid());
    }

    public MetadataId NextMetadataIdentity()
    {
        return new MetadataId(Guid.NewGuid());
    }

    public async Task AddAsync(SmartMeter meter)
    {
        await tenantDbContext.SmartMeters.AddAsync(meter);
        await tenantDbContext.SaveChangesAsync();
    }

    public async Task<List<SmartMeter>> GetSmartMetersAsync()
    {
        return await tenantDbContext.SmartMeters
            .Include(sm => sm.Metadata)
            .Where(smartMeter => !String.IsNullOrEmpty(smartMeter.Name))
            .ToListAsync();
    }

    public async Task<SmartMeter?> GetSmartMeterByIdAsync(SmartMeterId smartMeterId)
    {
        return await tenantDbContext.SmartMeters
            .Include(sm => sm.Metadata)
            .FirstOrDefaultAsync(sm => sm.Id.Equals(smartMeterId));
    }

    public async Task<SmartMeter?> GetSmartMeterByTenantAndIdAsync(Tenant tenant, SmartMeterId smartMeterId)
    {
        var tenantSpecificDbContext = tenantDbContextFactory.CreateDbContext(tenant.DatabaseName,
            databaseConfigOptions.Value.SuperUsername, databaseConfigOptions.Value.SuperUserPassword);

        return await tenantSpecificDbContext.SmartMeters.Include(m => m.Metadata)
            .FirstOrDefaultAsync(sm => sm.Id.Equals(smartMeterId));
    }

    public async Task<SmartMeter?> GetSmartMeterBySerialNumberAsync(ConnectorSerialNumber serialNumber)
    {
        return await tenantDbContext.SmartMeters
            .Include(sm => sm.Metadata)
            .FirstOrDefaultAsync(sm => sm.ConnectorSerialNumber.Equals(serialNumber));
    }

    public async Task UpdateAsync(SmartMeter smartMeter)
    {
        tenantDbContext.SmartMeters.Update(smartMeter);
        await tenantDbContext.SaveChangesAsync();
    }
}