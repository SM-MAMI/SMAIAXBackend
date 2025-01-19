using System.ComponentModel.DataAnnotations;

namespace SMAIAXBackend.Application.DTOs;

public class MeasurementListDto(
    List<MeasurementRawDto>? measurementRawDto,
    List<MeasurementAggregatedDto>? measurementAggregatedDto,
    int amountOfMeasurements)
{
    public List<MeasurementRawDto>? MeasurementRawList { get; set; } = measurementRawDto;
    public List<MeasurementAggregatedDto>? MeasurementAggregatedList { get; set; } = measurementAggregatedDto;

    [Required]
    public int AmountOfMeasurements { get; set; } = amountOfMeasurements;
}