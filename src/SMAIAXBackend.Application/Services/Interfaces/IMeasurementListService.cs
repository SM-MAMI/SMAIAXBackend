using SMAIAXBackend.Application.DTOs;
using SMAIAXBackend.Domain.Model.Enums;

namespace SMAIAXBackend.Application.Services.Interfaces;

public interface IMeasurementListService
{
    /// <summary>
    ///     Get the time-filtered measurements of a tenant by smart meter.
    /// </summary>
    /// <param name="smartMeterId">The specific smart meter id.</param>
    /// <param name="startAt">Timestamp filter. Data with a timestamp newer/greater than or equal to startAt will be returned.</param>
    /// <param name="endAt">Timestamp filter. Data with a timestamp older/smaller than or equal to endAt will be returned.</param>
    /// <returns>All measurements between given limitations</returns>
    Task<List<MeasurementDto>> GetMeasurementsBySmartMeterAsync(Guid smartMeterId,
        DateTime startAt, DateTime endAt);

    /// <summary>
    /// Get the time-filtered measurements of a tenant by smart meter and resolution.
    /// </summary>
    /// <param name="smartMeterId">The specific smart meter id.</param>
    /// <param name="measurementResolution">The resolution in which the measurements should be returned.</param>
    /// <param name="timeSpans">List of datetime tuples where the first value represents the start time and the second value represents the end time.
    /// If time-spans are null, then all data will be returned.</param>
    /// <returns>All measurements in the specified resolution and the given limitations.</returns>
    Task<List<MeasurementDto>> GetMeasurementsBySmartMeterAndResolutionAsync(Guid smartMeterId,
        MeasurementResolution measurementResolution, IList<(DateTime?, DateTime?)>? timeSpans = null);
}