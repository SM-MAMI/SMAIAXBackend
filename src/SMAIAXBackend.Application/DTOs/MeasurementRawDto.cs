using System.ComponentModel.DataAnnotations;

using SMAIAXBackend.Domain.Model.Entities;

namespace SMAIAXBackend.Application.DTOs;

public class MeasurementRawDto(
    DateTime timestamp,
    double voltagePhase1,
    double voltagePhase2,
    double voltagePhase3,
    double currentPhase1,
    double currentPhase2,
    double currentPhase3,
    double positiveActivePower,
    double negativeActivePower,
    double positiveReactiveEnergyTotal,
    double negativeReactiveEnergyTotal,
    double positiveActiveEnergyTotal,
    double negativeActiveEnergyTotal)
{
    [Required] public DateTime Timestamp { get; set; } = timestamp;
    [Required] public double VoltagePhase1 { get; set; } = voltagePhase1;
    [Required] public double VoltagePhase2 { get; set; } = voltagePhase2;
    [Required] public double VoltagePhase3 { get; set; } = voltagePhase3;
    [Required] public double CurrentPhase1 { get; set; } = currentPhase1;
    [Required] public double CurrentPhase2 { get; set; } = currentPhase2;
    [Required] public double CurrentPhase3 { get; set; } = currentPhase3;
    [Required] public double PositiveActivePower { get; set; } = positiveActivePower;
    [Required] public double NegativeActivePower { get; set; } = negativeActivePower;
    [Required] public double PositiveReactiveEnergyTotal { get; set; } = positiveReactiveEnergyTotal;
    [Required] public double NegativeReactiveEnergyTotal { get; set; } = negativeReactiveEnergyTotal;
    [Required] public double PositiveActiveEnergyTotal { get; set; } = positiveActiveEnergyTotal;
    [Required] public double NegativeActiveEnergyTotal { get; set; } = negativeActiveEnergyTotal;

    public static MeasurementRawDto FromMeasurement(Measurement measurement)
    {
        return new MeasurementRawDto(
            measurement.Timestamp,
            measurement.VoltagePhase1,
            measurement.VoltagePhase2,
            measurement.VoltagePhase3,
            measurement.CurrentPhase1,
            measurement.CurrentPhase2,
            measurement.CurrentPhase3,
            measurement.PositiveActivePower,
            measurement.NegativeActivePower,
            measurement.PositiveReactiveEnergyTotal,
            measurement.NegativeReactiveEnergyTotal,
            measurement.PositiveActiveEnergyTotal,
            measurement.NegativeActiveEnergyTotal);
    }
}