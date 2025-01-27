using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.Enums;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;

namespace SMAIAXBackend.Domain.Repositories;

public interface IMeasurementRepository
{
    /// <summary>
    ///     Reads all measurements of a tenant, and it's smart meter in the given time range and calculates the amount.
    /// </summary>
    /// <param name="smartMeterId">The specific smart meter id.</param>
    /// <param name="startAt">
    ///     Optional timestamp filter. Data with a timestamp newer/greater than or equal to startAt will be
    ///     returned.
    /// </param>
    /// <param name="endAt">
    ///     Optional timestamp filter. Data with a timestamp older/smaller than or equal to endAt will be
    ///     returned.
    /// </param>
    /// <param name="tenant">Optional tenant if measurements are not of the current tenant.</param>
    /// <returns>All measurements between given limitations and a count variable.</returns>
    Task<(List<Measurement>, int)> GetMeasurementsBySmartMeterAsync(SmartMeterId smartMeterId, DateTime? startAt,
        DateTime? endAt, Tenant? tenant = null);

    /// <summary>
    ///     Reads all measurements by resolution, which can not be of type RAW and by smart meter, in the given time range.
    /// </summary>
    /// <param name="smartMeterId">The specific smart meter id.</param>
    /// <param name="measurementResolution">The resolution determines the period over which the measurements were aggregated.</param>
    /// <param name="startAt">
    ///     Optional timestamp filter. Data with a timestamp newer/greater than or equal to startAt will be
    ///     returned.
    /// </param>
    /// <param name="endAt">
    ///     Optional timestamp filter. Data with a timestamp older/smaller than or equal to endAt will be
    ///     returned.
    /// </param>
    /// <param name="tenant">Optional tenant if measurements are not of the current tenant.</param>
    /// <returns>All measurements with diverse aggregations between given limitations and a count variable.</returns>
    Task<(List<AggregatedMeasurement>, int)> GetAggregatedMeasurementsBySmartMeterAsync(SmartMeterId smartMeterId,
        MeasurementResolution measurementResolution, DateTime? startAt, DateTime? endAt, Tenant? tenant = null);

    /// <summary>
    ///     Reads the amount of measurements by given smart meter id, resolution and time range.
    /// </summary>
    /// <param name="smartMeterId">The specific smart meter id.</param>
    /// <param name="measurementResolution">The resolution determines the period over which the measurements were aggregated.</param>
    /// <param name="startAt">
    ///     Optional timestamp filter. Data with a timestamp newer/greater than or equal to startAt will be
    ///     returned.
    /// </param>
    /// <param name="endAt">
    ///     Optional timestamp filter. Data with a timestamp older/smaller than or equal to endAt will be
    ///     returned.
    /// </param>
    /// <param name="tenant">Optional tenant. By default, the current tenant is used.</param>
    /// <returns>A count variable indicating the amount of measurements.</returns>
    Task<int> GetMeasurementCountBySmartMeterAndResolution(SmartMeterId smartMeterId,
        MeasurementResolution measurementResolution, DateTime? startAt,
        DateTime? endAt, Tenant? tenant = null);
}