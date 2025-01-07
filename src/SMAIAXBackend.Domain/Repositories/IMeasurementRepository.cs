using SMAIAXBackend.Domain.Model.Entities.Measurements;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;

namespace SMAIAXBackend.Domain.Repositories;

public interface IMeasurementRepository
{
    /// <summary>
    ///     Reads all measurements of a tenant, and it's smart meter in the given time range.
    /// </summary>
    /// <param name="smartMeterId">The specific smart meter id.</param>
    /// <param name="startAt">Optional timestamp filter. Data with a timestamp newer/greater than or equal to startAt will be returned.</param>
    /// <param name="endAt">Optional timestamp filter. Data with a timestamp older/smaller than or equal to endAt will be returned.</param>
    /// <returns>All measurements between given limitations.</returns>
    Task<List<Measurement>> GetMeasurementsBySmartMeterAsync(SmartMeterId smartMeterId, DateTime? startAt,
        DateTime? endAt);

    Task<List<MeasurementPerMinute>> GetMeasurementsPerMinuteBySmartMeterAsync(
        SmartMeterId smartMeterId,
        DateTime? startAt,
        DateTime? endAt);

    Task<List<MeasurementPerQuarterHour>> GetMeasurementsPerQuarterHourBySmartMeterAsync(
        SmartMeterId smartMeterId,
        DateTime? startAt,
        DateTime? endAt);

    Task<List<MeasurementPerHour>> GetMeasurementsPerHourBySmartMeterAsync(
        SmartMeterId smartMeterId,
        DateTime? startAt,
        DateTime? endAt);

    Task<List<MeasurementPerDay>> GetMeasurementsPerDayBySmartMeterAsync(
        SmartMeterId smartMeterId,
        DateTime? startAt,
        DateTime? endAt);

    Task<List<MeasurementPerWeek>> GetMeasurementsPerWeekBySmartMeterAsync(
        SmartMeterId smartMeterId,
        DateTime? startAt,
        DateTime? endAt);
}