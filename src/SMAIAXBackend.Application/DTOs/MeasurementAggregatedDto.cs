using System.ComponentModel.DataAnnotations;

using SMAIAXBackend.Domain.Model.Entities;

namespace SMAIAXBackend.Application.DTOs;

public class MeasurementAggregatedDto(
    DateTime timestamp,
    string uptime,
    double minPositiveActivePower,
    double maxPositiveActivePower,
    double avgPositiveActivePower,
    double medPositiveActivePower,
    double minPositiveActiveEnergyTotal,
    double maxPositiveActiveEnergyTotal,
    double avgPositiveActiveEnergyTotal,
    double medPositiveActiveEnergyTotal,
    double minNegativeActivePower,
    double maxNegativeActivePower,
    double avgNegativeActivePower,
    double medNegativeActivePower,
    double minNegativeActiveEnergyTotal,
    double maxNegativeActiveEnergyTotal,
    double avgNegativeActiveEnergyTotal,
    double medNegativeActiveEnergyTotal,
    double minReactiveEnergyQuadrant1Total,
    double maxReactiveEnergyQuadrant1Total,
    double avgReactiveEnergyQuadrant1Total,
    double medReactiveEnergyQuadrant1Total,
    double minReactiveEnergyQuadrant3Total,
    double maxReactiveEnergyQuadrant3Total,
    double avgReactiveEnergyQuadrant3Total,
    double medReactiveEnergyQuadrant3Total,
    double minTotalPower,
    double maxTotalPower,
    double avgTotalPower,
    double medTotalPower,
    double minCurrentPhase1,
    double maxCurrentPhase1,
    double avgCurrentPhase1,
    double medCurrentPhase1,
    double minVoltagePhase1,
    double maxVoltagePhase1,
    double avgVoltagePhase1,
    double medVoltagePhase1,
    double minCurrentPhase2,
    double maxCurrentPhase2,
    double avgCurrentPhase2,
    double medCurrentPhase2,
    double minVoltagePhase2,
    double maxVoltagePhase2,
    double avgVoltagePhase2,
    double medVoltagePhase2,
    double minCurrentPhase3,
    double maxCurrentPhase3,
    double avgCurrentPhase3,
    double medCurrentPhase3,
    double minVoltagePhase3,
    double maxVoltagePhase3,
    double avgVoltagePhase3,
    double medVoltagePhase3,
    int amountOfMeasurements)
{
    [Required]
    public DateTime Timestamp { get; set; } = timestamp;
    [Required]
    public string Uptime { get; set; } = uptime;

    [Required]
    public double MinPositiveActivePower { get; set; } = minPositiveActivePower;
    [Required]
    public double MaxPositiveActivePower { get; set; } = maxPositiveActivePower;
    [Required]
    public double AvgPositiveActivePower { get; set; } = avgPositiveActivePower;
    [Required]
    public double MedPositiveActivePower { get; set; } = medPositiveActivePower;

    [Required]
    public double MinPositiveActiveEnergyTotal { get; set; } = minPositiveActiveEnergyTotal;
    [Required]
    public double MaxPositiveActiveEnergyTotal { get; set; } = maxPositiveActiveEnergyTotal;
    [Required]
    public double AvgPositiveActiveEnergyTotal { get; set; } = avgPositiveActiveEnergyTotal;
    [Required]
    public double MedPositiveActiveEnergyTotal { get; set; } = medPositiveActiveEnergyTotal;

    [Required]
    public double MinNegativeActivePower { get; set; } = minNegativeActivePower;
    [Required]
    public double MaxNegativeActivePower { get; set; } = maxNegativeActivePower;
    [Required]
    public double AvgNegativeActivePower { get; set; } = avgNegativeActivePower;
    [Required]
    public double MedNegativeActivePower { get; set; } = medNegativeActivePower;

    [Required]
    public double MinNegativeActiveEnergyTotal { get; set; } = minNegativeActiveEnergyTotal;
    [Required]
    public double MaxNegativeActiveEnergyTotal { get; set; } = maxNegativeActiveEnergyTotal;
    [Required]
    public double AvgNegativeActiveEnergyTotal { get; set; } = avgNegativeActiveEnergyTotal;
    [Required]
    public double MedNegativeActiveEnergyTotal { get; set; } = medNegativeActiveEnergyTotal;

    [Required]
    public double MinReactiveEnergyQuadrant1Total { get; set; } = minReactiveEnergyQuadrant1Total;
    [Required]
    public double MaxReactiveEnergyQuadrant1Total { get; set; } = maxReactiveEnergyQuadrant1Total;
    [Required]
    public double AvgReactiveEnergyQuadrant1Total { get; set; } = avgReactiveEnergyQuadrant1Total;
    [Required]
    public double MedReactiveEnergyQuadrant1Total { get; set; } = medReactiveEnergyQuadrant1Total;

    [Required]
    public double MinReactiveEnergyQuadrant3Total { get; set; } = minReactiveEnergyQuadrant3Total;
    [Required]
    public double MaxReactiveEnergyQuadrant3Total { get; set; } = maxReactiveEnergyQuadrant3Total;
    [Required]
    public double AvgReactiveEnergyQuadrant3Total { get; set; } = avgReactiveEnergyQuadrant3Total;
    [Required]
    public double MedReactiveEnergyQuadrant3Total { get; set; } = medReactiveEnergyQuadrant3Total;

    [Required]
    public double MinTotalPower { get; set; } = minTotalPower;
    [Required]
    public double MaxTotalPower { get; set; } = maxTotalPower;
    [Required]
    public double AvgTotalPower { get; set; } = avgTotalPower;
    [Required]
    public double MedTotalPower { get; set; } = medTotalPower;

    [Required]
    public double MinCurrentPhase1 { get; set; } = minCurrentPhase1;
    [Required]
    public double MaxCurrentPhase1 { get; set; } = maxCurrentPhase1;
    [Required]
    public double AvgCurrentPhase1 { get; set; } = avgCurrentPhase1;
    [Required]
    public double MedCurrentPhase1 { get; set; } = medCurrentPhase1;

    [Required]
    public double MinVoltagePhase1 { get; set; } = minVoltagePhase1;
    [Required]
    public double MaxVoltagePhase1 { get; set; } = maxVoltagePhase1;
    [Required]
    public double AvgVoltagePhase1 { get; set; } = avgVoltagePhase1;
    [Required]
    public double MedVoltagePhase1 { get; set; } = medVoltagePhase1;

    [Required]
    public double MinCurrentPhase2 { get; set; } = minCurrentPhase2;
    [Required]
    public double MaxCurrentPhase2 { get; set; } = maxCurrentPhase2;
    [Required]
    public double AvgCurrentPhase2 { get; set; } = avgCurrentPhase2;
    [Required]
    public double MedCurrentPhase2 { get; set; } = medCurrentPhase2;

    [Required]
    public double MinVoltagePhase2 { get; set; } = minVoltagePhase2;
    [Required]
    public double MaxVoltagePhase2 { get; set; } = maxVoltagePhase2;
    [Required]
    public double AvgVoltagePhase2 { get; set; } = avgVoltagePhase2;
    [Required]
    public double MedVoltagePhase2 { get; set; } = medVoltagePhase2;

    [Required]
    public double MinCurrentPhase3 { get; set; } = minCurrentPhase3;
    [Required]
    public double MaxCurrentPhase3 { get; set; } = maxCurrentPhase3;
    [Required]
    public double AvgCurrentPhase3 { get; set; } = avgCurrentPhase3;
    [Required]
    public double MedCurrentPhase3 { get; set; } = medCurrentPhase3;

    [Required]
    public double MinVoltagePhase3 { get; set; } = minVoltagePhase3;
    [Required]
    public double MaxVoltagePhase3 { get; set; } = maxVoltagePhase3;
    [Required]
    public double AvgVoltagePhase3 { get; set; } = avgVoltagePhase3;
    [Required]
    public double MedVoltagePhase3 { get; set; } = medVoltagePhase3;

    [Required]
    public int AmountOfMeasurements { get; set; } = amountOfMeasurements;

    public static MeasurementAggregatedDto FromAggregatedMeasurement(AggregatedMeasurement measurement)
    {
        return new MeasurementAggregatedDto(
            measurement.Timestamp,
            measurement.Uptime,
            measurement.MinPositiveActivePower,
            measurement.MaxPositiveActivePower,
            measurement.AvgPositiveActivePower,
            measurement.MedPositiveActivePower,
            measurement.MinPositiveActiveEnergyTotal,
            measurement.MaxPositiveActiveEnergyTotal,
            measurement.AvgPositiveActiveEnergyTotal,
            measurement.MedPositiveActiveEnergyTotal,
            measurement.MinNegativeActivePower,
            measurement.MaxNegativeActivePower,
            measurement.AvgNegativeActivePower,
            measurement.MedNegativeActivePower,
            measurement.MinNegativeActiveEnergyTotal,
            measurement.MaxNegativeActiveEnergyTotal,
            measurement.AvgNegativeActiveEnergyTotal,
            measurement.MedNegativeActiveEnergyTotal,
            measurement.MinReactiveEnergyQuadrant1Total,
            measurement.MaxReactiveEnergyQuadrant1Total,
            measurement.AvgReactiveEnergyQuadrant1Total,
            measurement.MedReactiveEnergyQuadrant1Total,
            measurement.MinReactiveEnergyQuadrant3Total,
            measurement.MaxReactiveEnergyQuadrant3Total,
            measurement.AvgReactiveEnergyQuadrant3Total,
            measurement.MedReactiveEnergyQuadrant3Total,
            measurement.MinTotalPower,
            measurement.MaxTotalPower,
            measurement.AvgTotalPower,
            measurement.MedTotalPower,
            measurement.MinCurrentPhase1,
            measurement.MaxCurrentPhase1,
            measurement.AvgCurrentPhase1,
            measurement.MedCurrentPhase1,
            measurement.MinVoltagePhase1,
            measurement.MaxVoltagePhase1,
            measurement.AvgVoltagePhase1,
            measurement.MedVoltagePhase1,
            measurement.MinCurrentPhase2,
            measurement.MaxCurrentPhase2,
            measurement.AvgCurrentPhase2,
            measurement.MedCurrentPhase2,
            measurement.MinVoltagePhase2,
            measurement.MaxVoltagePhase2,
            measurement.AvgVoltagePhase2,
            measurement.MedVoltagePhase2,
            measurement.MinCurrentPhase3,
            measurement.MaxCurrentPhase3,
            measurement.AvgCurrentPhase3,
            measurement.MedCurrentPhase3,
            measurement.MinVoltagePhase3,
            measurement.MaxVoltagePhase3,
            measurement.AvgVoltagePhase3,
            measurement.MedVoltagePhase3,
            measurement.AmountOfMeasurements
        );
    }
}