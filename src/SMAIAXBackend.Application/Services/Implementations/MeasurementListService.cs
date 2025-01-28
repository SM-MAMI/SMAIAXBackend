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
    ISmartMeterRepository smartMeterRepository) : IMeasurementListService
{
    public async Task<MeasurementListDto> GetMeasurementsBySmartMeterAndResolutionAsync(
        Guid smartMeterId, MeasurementResolution measurementResolution, DateTime? startAt, DateTime? endAt, Tenant? tenant = null)
    {
        return await GetMeasurementsBySmartMeterAndResolutionAsync(smartMeterId, measurementResolution,
            new List<(DateTime?, DateTime?)> { (startAt, endAt) }, tenant);
    }

    public async Task<MeasurementListDto> GetMeasurementsBySmartMeterAndResolutionAsync(Guid smartMeterId,
        MeasurementResolution measurementResolution,
        IList<(DateTime?, DateTime?)>? timeSpans = null, Tenant? tenant = null)
    {
        // check if smart meter exists.
        var smartMeter = tenant == null
            ? await smartMeterRepository.GetSmartMeterByIdAsync(new SmartMeterId(smartMeterId))
            : await smartMeterRepository.GetSmartMeterByTenantAndIdAsync(tenant, new SmartMeterId(smartMeterId));
        if (smartMeter == null)
        {
            throw new SmartMeterNotFoundException(new SmartMeterId(smartMeterId));
        }

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
                    new SmartMeterId(smartMeterId), span.Item1, span.Item2, tenant);
                measurements.AddRange(currentMeasurements);
                count += currentCount;
            }

            return new MeasurementListDto(measurements.Select(MeasurementRawDto.FromMeasurement).ToList(), null, count);
        }
        else
        {
            var aggregateMeasurements = new List<AggregatedMeasurement>();
            var count = 0;
            foreach (var span in timeSpans)
            {
                var (currentMeasurements, currentCount) =
                    await measurementRepository.GetAggregatedMeasurementsBySmartMeterAsync(
                        new SmartMeterId(smartMeterId), measurementResolution, span.Item1,
                        span.Item2, tenant);
                aggregateMeasurements.AddRange(currentMeasurements);
                count += currentCount;
            }

            return new MeasurementListDto(null,
                aggregateMeasurements.Select(MeasurementAggregatedDto.FromAggregatedMeasurement).ToList(), count);
        }
    }
}