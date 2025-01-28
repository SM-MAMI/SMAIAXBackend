using System.ComponentModel.DataAnnotations;

using SMAIAXBackend.Domain.Model.Entities;

namespace SMAIAXBackend.Application.DTOs;

public class MeasurementRawDto(
    DateTime timestamp,
    double positiveActivePower,
    double positiveActiveEnergyTotal,
    double negativeActivePower,
    double negativeActiveEnergyTotal,
    double reactiveEnergyQuadrant1Total,
    double reactiveEnergyQuadrant3Total,
    double currentPhase1,
    double voltagePhase1,
    double currentPhase2,
    double voltagePhase2,
    double currentPhase3,
    double voltagePhase3)
{
    [Required] public DateTime Timestamp { get; set; } = timestamp;
    [Required] public double PositiveActivePower { get; set; } = positiveActivePower;
    [Required] public double PositiveActiveEnergyTotal { get; set; } = positiveActiveEnergyTotal;
    [Required] public double NegativeActivePower { get; set; } = negativeActivePower;
    [Required] public double NegativeActiveEnergyTotal { get; set; } = negativeActiveEnergyTotal;
    [Required] public double ReactiveEnergyQuadrant1Total { get; set; } = reactiveEnergyQuadrant1Total;
    [Required] public double ReactiveEnergyQuadrant3Total { get; set; } = reactiveEnergyQuadrant3Total;
    [Required] public double CurrentPhase1 { get; set; } = currentPhase1;
    [Required] public double VoltagePhase1 { get; set; } = voltagePhase1;
    [Required] public double CurrentPhase2 { get; set; } = currentPhase2;
    [Required] public double VoltagePhase2 { get; set; } = voltagePhase2;
    [Required] public double CurrentPhase3 { get; set; } = currentPhase3;
    [Required] public double VoltagePhase3 { get; set; } = voltagePhase3;

    public static MeasurementRawDto FromMeasurement(Measurement measurement)
    {
        return new MeasurementRawDto(
            measurement.Timestamp,
            measurement.PositiveActivePower, measurement.PositiveActiveEnergyTotal,
            measurement.NegativeActivePower, measurement.NegativeActiveEnergyTotal,
            measurement.ReactiveEnergyQuadrant1Total, measurement.ReactiveEnergyQuadrant3Total,
            measurement.CurrentPhase1, measurement.VoltagePhase1,
            measurement.CurrentPhase2, measurement.VoltagePhase2, measurement.CurrentPhase3, measurement.VoltagePhase3);
    }
}