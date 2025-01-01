using Microsoft.EntityFrameworkCore;

using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.Enums;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;
using SMAIAXBackend.Domain.Repositories;
using SMAIAXBackend.Infrastructure.DbContexts;
using SMAIAXBackend.Infrastructure.Repositories.Extensions;

namespace SMAIAXBackend.Infrastructure.Repositories;

public class MeasurementRepository(
    TenantDbContext tenantDbContext) : IMeasurementRepository
{
    public async Task<List<Measurement>> GetMeasurementsBySmartMeterAsync(SmartMeterId smartMeterId,
        DateTime startAt,
        DateTime endAt)
    {
        return await tenantDbContext.Measurements.AsNoTracking().Where(m => m.SmartMeterId.Equals(smartMeterId))
            .Where(m => m.Timestamp >= startAt && m.Timestamp <= endAt).OrderByDescending(m => m.Timestamp)
            .ToListAsync();
    }

    public async Task<List<Measurement>> GetMeasurementsBySmartMeterAndResolutionAsync(SmartMeterId smartMeterId,
        MeasurementResolution measurementResolution,
        DateTime? startAt,
        DateTime? endAt)
    {
        return await tenantDbContext.Measurements.GroupTimestamps(measurementResolution)
            .AsNoTracking().Where(m => m.SmartMeterId.Equals(smartMeterId))
            .Where(m => startAt == null || m.Timestamp >= startAt)
            .Where(m => endAt == null || m.Timestamp <= endAt)
            .OrderByDescending(m => m.Timestamp)
            .ToListAsync();
    }
}