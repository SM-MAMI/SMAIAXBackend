using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.Entities.Measurements;
using SMAIAXBackend.Domain.Model.Enums;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;
using SMAIAXBackend.Domain.Repositories;
using SMAIAXBackend.Domain.Specifications;

namespace SMAIAXBackend.Domain.Handlers;

public class MeasurementHandler(
    ISmartMeterRepository smartMeterRepository,
    IMeasurementRepository measurementRepository) : IMeasurementHandler
{
    public async Task<IList<MeasurementBase>> GetMeasurementsByPolicyAsync(Policy policy)
    {
        var smartMeter = await smartMeterRepository.GetSmartMeterByIdAsync(policy.SmartMeterId);
        if (smartMeter == null)
        {
            return Array.Empty<MeasurementBase>();
        }

        var timeSpans = new List<Span>();
        if (policy.LocationResolution == LocationResolution.None)
        {
            // If location resolution "does not matter", return all measurements.
            timeSpans.Add(new Span(null, null));
        }
        else
        {
            // Otherwise location resolution must match with metadata.
            var metadata = smartMeter.Metadata.OrderBy(m => m.ValidFrom).ToList();
            if (metadata.Count == 0)
            {
                // If no metadata given, policy location resolution can not match because there is no location reference for the measurements.
                return Array.Empty<MeasurementBase>();
            }

            var specification = new LocationResolutionSpecification(policy.LocationResolution);
            for (var i = 0; i < metadata!.Count; i += 1)
            {
                if (!specification.IsSatisfiedBy(metadata[i]))
                {
                    continue;
                }

                var nextIndex = i + 1;
                timeSpans.Add(new Span(metadata[i].ValidFrom,
                    nextIndex >= metadata!.Count ? null : metadata[i + 1].ValidFrom));
            }
        }


        return await GetMeasurementsByResolutionAndTimeSpans(policy.MeasurementResolution, timeSpans, smartMeter.Id);
    }

    private async Task<IList<MeasurementBase>> GetMeasurementsByResolutionAndTimeSpans(
        MeasurementResolution measurementResolution,
        List<Span> timeSpans, SmartMeterId smartMeterId)
    {
        var measurements = new List<MeasurementBase>();
        switch (measurementResolution)
        {
            case MeasurementResolution.Minute:
                foreach (var span in timeSpans)
                {
                    measurements.AddRange(
                        await measurementRepository.GetMeasurementsPerMinuteBySmartMeterAsync(smartMeterId, span.Start,
                            span.End));
                }
                break;
            case MeasurementResolution.QuarterHour:
                foreach (var span in timeSpans)
                {
                    measurements.AddRange(
                        await measurementRepository.GetMeasurementsPerQuarterHourBySmartMeterAsync(smartMeterId,
                            span.Start, span.End));
                }
                break;
            case MeasurementResolution.Hour:
                foreach (var span in timeSpans)
                {
                    measurements.AddRange(
                        await measurementRepository.GetMeasurementsPerHourBySmartMeterAsync(smartMeterId, span.Start,
                            span.End));
                }
                break;
            case MeasurementResolution.Day:
                foreach (var span in timeSpans)
                {
                    measurements.AddRange(
                        await measurementRepository.GetMeasurementsPerDayBySmartMeterAsync(smartMeterId, span.Start,
                            span.End));
                }
                break;
            case MeasurementResolution.Week:
                foreach (var span in timeSpans)
                {
                    measurements.AddRange(
                        await measurementRepository.GetMeasurementsPerWeekBySmartMeterAsync(smartMeterId, span.Start,
                            span.End));
                }
                break;
            default:
                foreach (var span in timeSpans)
                {
                    measurements.AddRange(
                        await measurementRepository.GetMeasurementsBySmartMeterAsync(smartMeterId, span.Start,
                            span.End));
                }

                break;
        }

        return measurements;
    }

    private sealed class Span(DateTime? start, DateTime? end)
    {
        public DateTime? Start => start;
        public DateTime? End => end;
    }
}