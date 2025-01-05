using SMAIAXBackend.Application.DTOs;
using SMAIAXBackend.Application.Exceptions;
using SMAIAXBackend.Application.Services.Interfaces;
using SMAIAXBackend.Domain.Model.Entities.Measurements;
using SMAIAXBackend.Domain.Model.Enums;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;
using SMAIAXBackend.Domain.Repositories;

namespace SMAIAXBackend.Application.Services.Implementations;

public class MeasurementListService(
    IMeasurementRepository measurementRepository,
    ISmartMeterListService smartMeterListService) : IMeasurementListService
{
    public async Task<List<MeasurementDto>> GetMeasurementsBySmartMeterAsync(
        Guid smartMeterId, DateTime startAt, DateTime endAt)
    {
        return await GetMeasurementsBySmartMeterAndResolutionAsync(smartMeterId, MeasurementResolution.Raw,
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

        var measurements = new List<MeasurementBase>();
        switch (measurementResolution)
        {
            case MeasurementResolution.Minute:
                foreach (var span in timeSpans)
                {
                    measurements.AddRange(
                        await measurementRepository.GetMeasurementsPerMinuteBySmartMeterAsync(
                            new SmartMeterId(smartMeterId), span.Item1,
                            span.Item2));
                }

                break;
            case MeasurementResolution.QuarterHour:
                foreach (var span in timeSpans)
                {
                    measurements.AddRange(
                        await measurementRepository.GetMeasurementsPerQuarterHourBySmartMeterAsync(
                            new SmartMeterId(smartMeterId),
                            span.Item1, span.Item2));
                }

                break;
            case MeasurementResolution.Hour:
                foreach (var span in timeSpans)
                {
                    measurements.AddRange(
                        await measurementRepository.GetMeasurementsPerHourBySmartMeterAsync(
                            new SmartMeterId(smartMeterId), span.Item1,
                            span.Item2));
                }

                break;
            case MeasurementResolution.Day:
                foreach (var span in timeSpans)
                {
                    measurements.AddRange(
                        await measurementRepository.GetMeasurementsPerDayBySmartMeterAsync(
                            new SmartMeterId(smartMeterId), span.Item1,
                            span.Item2));
                }

                break;
            case MeasurementResolution.Week:
                foreach (var span in timeSpans)
                {
                    measurements.AddRange(
                        await measurementRepository.GetMeasurementsPerWeekBySmartMeterAsync(
                            new SmartMeterId(smartMeterId), span.Item1,
                            span.Item2));
                }

                break;
            default:
                foreach (var span in timeSpans)
                {
                    measurements.AddRange(
                        await measurementRepository.GetMeasurementsBySmartMeterAsync(new SmartMeterId(smartMeterId),
                            span.Item1,
                            span.Item2));
                }

                break;
        }

        return measurements.Select(MeasurementDto.FromMeasurement).ToList();
    }
}