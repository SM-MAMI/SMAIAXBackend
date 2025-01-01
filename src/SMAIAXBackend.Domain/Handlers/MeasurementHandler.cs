using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.Enums;
using SMAIAXBackend.Domain.Repositories;
using SMAIAXBackend.Domain.Specifications;

namespace SMAIAXBackend.Domain.Handlers;

public class MeasurementHandler(
    ISmartMeterRepository smartMeterRepository,
    IMeasurementRepository measurementRepository) : IMeasurementHandler
{
    public async Task<IList<Measurement>> GetMeasurementsByPolicyAsync(Policy policy)
    {
        var smartMeter = await smartMeterRepository.GetSmartMeterByIdAsync(policy.SmartMeterId);
        if (smartMeter == null)
        {
            return Array.Empty<Measurement>();
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
            var metadata = smartMeter.Metadata.OrderByDescending(m => m.ValidFrom).ToList();
            if (metadata.Count == 0)
            {
                // If no metadata given, policy location resolution can not match because there is no location reference for the measurements.
                return Array.Empty<Measurement>();
            }

            var specification = new LocationResolutionSpecification(policy.LocationResolution);
            for (var i = 0; i < metadata!.Count; i += 1)
            {
                if (!specification.IsSatisfiedBy(metadata[i]))
                {
                    continue;
                }

                var nextIndex = i + 1;
                // reversed order
                timeSpans.Add(new Span(nextIndex >= metadata!.Count ? null : metadata[i + 1].ValidFrom,
                    metadata[i].ValidFrom));
            }
        }

        var measurements = new List<Measurement>();
        foreach (Span span in timeSpans)
        {
            var mes = await measurementRepository.GetMeasurementsBySmartMeterAndResolutionAsync(smartMeter.Id,
                policy.MeasurementResolution, span.Start, span.End);
            measurements.AddRange(mes);
        }

        return measurements;
    }

    private sealed class Span(DateTime? start, DateTime? end)
    {
        public DateTime? Start => start;
        public DateTime? End => end;
    }
}