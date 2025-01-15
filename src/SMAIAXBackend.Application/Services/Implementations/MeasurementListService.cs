using SMAIAXBackend.Application.DTOs;
using SMAIAXBackend.Application.Exceptions;
using SMAIAXBackend.Application.Services.Interfaces;
using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.Enums;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;
using SMAIAXBackend.Domain.Repositories;

namespace SMAIAXBackend.Application.Services.Implementations;

public class MeasurementListService(
    IMeasurementRepository measurementRepository,
    ISmartMeterListService smartMeterListService) : IMeasurementListService
{
    public async Task<List<MeasurementDto>> GetMeasurementsBySmartMeterAndResolutionAsync(
        Guid smartMeterId, MeasurementResolution measurementResolution, DateTime? startAt, DateTime? endAt)
    {
        return await GetMeasurementsBySmartMeterAndResolutionAsync(smartMeterId, measurementResolution,
            new List<(DateTime?, DateTime?)> { (startAt, endAt) });
    }

    public async Task<List<MeasurementDto>> GetMeasurementsBySmartMeterAndResolutionAsync(Guid smartMeterId,
        MeasurementResolution measurementResolution,
        IList<(DateTime?, DateTime?)>? timeSpans = null)
    {
        // check if smart meter exists.
        await smartMeterListService.GetSmartMeterByIdAsync(smartMeterId);

        if (timeSpans == null)
        {
            timeSpans = new (DateTime?, DateTime?)[] { (null, null) };
        }
        else
        {
            // check if timespans are valid
            if (timeSpans.Where(ts => ts is { Item1: not null, Item2: not null })
                .Any(timeSpan => timeSpan.Item2 < timeSpan.Item1))
            {
                throw new InvalidTimeRangeException();
            }
        }

        if (measurementResolution == MeasurementResolution.Raw)
        {
            var measurements = new List<Measurement>();
            var count = 0;
            foreach (var span in timeSpans)
            {
                var (currentMeasurements, currentCount) = await measurementRepository.GetMeasurementsBySmartMeterAsync(
                    new SmartMeterId(smartMeterId), span.Item1, span.Item2);
                measurements.AddRange(currentMeasurements);
                count += currentCount;
            }

            return measurements.Select(MeasurementDto.FromMeasurement).ToList();
        }
        else
        {
            var aggregateMeasurements = new List<AggregatedMeasurement>();
            var count = 0;
            foreach (var span in timeSpans)
            {
                var (currentMeasurements, currentCount) =
                    await measurementRepository.GetMeasurementsBySmartMeterAndResolutionAsync(
                        new SmartMeterId(smartMeterId), measurementResolution, span.Item1,
                        span.Item2);
                aggregateMeasurements.AddRange(currentMeasurements);
                count += currentCount;
            }

            return aggregateMeasurements.Select(MeasurementDto.FromAggregateMeasurement).ToList();
        }
    }
}