using Microsoft.EntityFrameworkCore;

using SMAIAXBackend.Domain.Model.Entities.Measurements;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;
using SMAIAXBackend.Domain.Repositories;
using SMAIAXBackend.Infrastructure.DbContexts;

namespace SMAIAXBackend.Infrastructure.Repositories;

public class MeasurementRepository(
    TenantDbContext tenantDbContext) : IMeasurementRepository
{
    public async Task<List<Measurement>> GetMeasurementsBySmartMeterAsync(SmartMeterId smartMeterId,
        DateTime? startAt,
        DateTime? endAt)
    {
        return await tenantDbContext.Measurements.AsNoTracking()
            .Where(m => m.SmartMeterId.Equals(smartMeterId))
            .Where(m => startAt == null || m.Timestamp >= startAt)
            .Where(m => endAt == null || m.Timestamp <= endAt)
            .ToListAsync();
    }

    public async Task<List<MeasurementPerMinute>> GetMeasurementsPerMinuteBySmartMeterAsync(
        SmartMeterId smartMeterId,
        DateTime? startAt,
        DateTime? endAt)
    {
        return await tenantDbContext.MeasurementsPerMinute.AsNoTracking()
            .Where(m => m.SmartMeterId.Equals(smartMeterId))
            .Where(m => startAt == null || m.Timestamp >= startAt)
            .Where(m => endAt == null || m.Timestamp <= endAt)
            .ToListAsync();
    }

    public async Task<List<MeasurementPerQuarterHour>> GetMeasurementsPerQuarterHourBySmartMeterAsync(
        SmartMeterId smartMeterId, DateTime? startAt, DateTime? endAt)
    {
        return await tenantDbContext.MeasurementsPerQuarterHour.AsNoTracking()
            .Where(m => m.SmartMeterId.Equals(smartMeterId))
            .Where(m => startAt == null || m.Timestamp >= startAt)
            .Where(m => endAt == null || m.Timestamp <= endAt)
            .ToListAsync();
    }

    public async Task<List<MeasurementPerHour>> GetMeasurementsPerHourBySmartMeterAsync(SmartMeterId smartMeterId,
        DateTime? startAt, DateTime? endAt)
    {
        return await tenantDbContext.MeasurementsPerHour.AsNoTracking()
            .Where(m => m.SmartMeterId.Equals(smartMeterId))
            .Where(m => startAt == null || m.Timestamp >= startAt)
            .Where(m => endAt == null || m.Timestamp <= endAt)
            .ToListAsync();
    }

    public async Task<List<MeasurementPerDay>> GetMeasurementsPerDayBySmartMeterAsync(SmartMeterId smartMeterId,
        DateTime? startAt, DateTime? endAt)
    {
        return await tenantDbContext.MeasurementsPerDay.AsNoTracking()
            .Where(m => m.SmartMeterId.Equals(smartMeterId))
            .Where(m => startAt == null || m.Timestamp >= startAt)
            .Where(m => endAt == null || m.Timestamp <= endAt)
            .ToListAsync();
    }

    public async Task<List<MeasurementPerWeek>> GetMeasurementsPerWeekBySmartMeterAsync(SmartMeterId smartMeterId,
        DateTime? startAt, DateTime? endAt)
    {
        return await tenantDbContext.MeasurementsPerWeek.AsNoTracking()
            .Where(m => m.SmartMeterId.Equals(smartMeterId))
            .Where(m => startAt == null || m.Timestamp >= startAt)
            .Where(m => endAt == null || m.Timestamp <= endAt)
            .ToListAsync();
    }
}