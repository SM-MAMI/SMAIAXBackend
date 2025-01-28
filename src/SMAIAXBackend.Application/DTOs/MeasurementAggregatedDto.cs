using System.ComponentModel.DataAnnotations;

using SMAIAXBackend.Domain.Model.Entities;

namespace SMAIAXBackend.Application.DTOs;

public class MeasurementAggregatedDto(
    DateTime timestamp,
    double minVoltagePhase1,
    double maxVoltagePhase1,
    double avgVoltagePhase1,
    double medVoltagePhase1,
    double minVoltagePhase2,
    double maxVoltagePhase2,
    double avgVoltagePhase2,
    double medVoltagePhase2,
    double minVoltagePhase3,
    double maxVoltagePhase3,
    double avgVoltagePhase3,
    double medVoltagePhase3,
    double minCurrentPhase1,
    double maxCurrentPhase1,
    double avgCurrentPhase1,
    double medCurrentPhase1,
    double minCurrentPhase2,
    double maxCurrentPhase2,
    double avgCurrentPhase2,
    double medCurrentPhase2,
    double minCurrentPhase3,
    double maxCurrentPhase3,
    double avgCurrentPhase3,
    double medCurrentPhase3,
    double minPositiveActivePower,
    double maxPositiveActivePower,
    double avgPositiveActivePower,
    double medPositiveActivePower,
    double minNegativeActivePower,
    double maxNegativeActivePower,
    double avgNegativeActivePower,
    double medNegativeActivePower,
    double minPositiveReactiveEnergyTotal,
    double maxPositiveReactiveEnergyTotal,
    double avgPositiveReactiveEnergyTotal,
    double medPositiveReactiveEnergyTotal,
    double minNegativeReactiveEnergyTotal,
    double maxNegativeReactiveEnergyTotal,
    double avgNegativeReactiveEnergyTotal,
    double medNegativeReactiveEnergyTotal,
    double minPositiveActiveEnergyTotal,
    double maxPositiveActiveEnergyTotal,
    double avgPositiveActiveEnergyTotal,
    double medPositiveActiveEnergyTotal,
    double minNegativeActiveEnergyTotal,
    double maxNegativeActiveEnergyTotal,
    double avgNegativeActiveEnergyTotal,
    double medNegativeActiveEnergyTotal,
    int amountOfMeasurements)
{
    [Required]
    public DateTime Timestamp { get; set; } = timestamp;

    #region VoltagePhase1

    [Required]
    public double MinVoltagePhase1 { get; set; } = minVoltagePhase1;
    [Required]
    public double MaxVoltagePhase1 { get; set; } = maxVoltagePhase1;
    [Required]
    public double AvgVoltagePhase1 { get; set; } = avgVoltagePhase1;
    [Required]
    public double MedVoltagePhase1 { get; set; } = medVoltagePhase1;

    #endregion

    #region VoltagePhase2

    [Required]
    public double MinVoltagePhase2 { get; set; } = minVoltagePhase2;
    [Required]
    public double MaxVoltagePhase2 { get; set; } = maxVoltagePhase2;
    [Required]
    public double AvgVoltagePhase2 { get; set; } = avgVoltagePhase2;
    [Required]
    public double MedVoltagePhase2 { get; set; } = medVoltagePhase2;

    #endregion


    #region VoltagePhase3

    [Required]
    public double MinVoltagePhase3 { get; set; } = minVoltagePhase3;
    [Required]
    public double MaxVoltagePhase3 { get; set; } = maxVoltagePhase3;
    [Required]
    public double AvgVoltagePhase3 { get; set; } = avgVoltagePhase3;
    [Required]
    public double MedVoltagePhase3 { get; set; } = medVoltagePhase3;

    #endregion

    #region CurrentPhase1

    [Required]
    public double MinCurrentPhase1 { get; set; } = minCurrentPhase1;
    [Required]
    public double MaxCurrentPhase1 { get; set; } = maxCurrentPhase1;
    [Required]
    public double AvgCurrentPhase1 { get; set; } = avgCurrentPhase1;
    [Required]
    public double MedCurrentPhase1 { get; set; } = medCurrentPhase1;

    #endregion

    #region CurrentPhase2

    [Required]
    public double MinCurrentPhase2 { get; set; } = minCurrentPhase2;
    [Required]
    public double MaxCurrentPhase2 { get; set; } = maxCurrentPhase2;
    [Required]
    public double AvgCurrentPhase2 { get; set; } = avgCurrentPhase2;
    [Required]
    public double MedCurrentPhase2 { get; set; } = medCurrentPhase2;

    #endregion

    #region CurrentPhase3

    [Required]
    public double MinCurrentPhase3 { get; set; } = minCurrentPhase3;
    [Required]
    public double MaxCurrentPhase3 { get; set; } = maxCurrentPhase3;
    [Required]
    public double AvgCurrentPhase3 { get; set; } = avgCurrentPhase3;
    [Required]
    public double MedCurrentPhase3 { get; set; } = medCurrentPhase3;

    #endregion

    #region PositiveActivePower

    [Required]
    public double MinPositiveActivePower { get; set; } = minPositiveActivePower;
    [Required]
    public double MaxPositiveActivePower { get; set; } = maxPositiveActivePower;
    [Required]
    public double AvgPositiveActivePower { get; set; } = avgPositiveActivePower;
    [Required]
    public double MedPositiveActivePower { get; set; } = medPositiveActivePower;

    #endregion

    #region NegativeActivePower

    [Required]
    public double MinNegativeActivePower { get; set; } = minNegativeActivePower;
    [Required]
    public double MaxNegativeActivePower { get; set; } = maxNegativeActivePower;
    [Required]
    public double AvgNegativeActivePower { get; set; } = avgNegativeActivePower;
    [Required]
    public double MedNegativeActivePower { get; set; } = medNegativeActivePower;

    #endregion

    #region PositiveReactiveEnergyTotal

    [Required]
    public double MinPositiveReactiveEnergyTotal { get; set; } = minPositiveReactiveEnergyTotal;
    [Required]
    public double MaxPositiveReactiveEnergyTotal { get; set; } = maxPositiveReactiveEnergyTotal;
    [Required]
    public double AvgPositiveReactiveEnergyTotal { get; set; } = avgPositiveReactiveEnergyTotal;
    [Required]
    public double MedPositiveReactiveEnergyTotal { get; set; } = medPositiveReactiveEnergyTotal;

    #endregion

    #region NegativeReactiveEnergyTotal

    [Required]
    public double MinNegativeReactiveEnergyTotal { get; set; } = minNegativeReactiveEnergyTotal;
    [Required]
    public double MaxNegativeReactiveEnergyTotal { get; set; } = maxNegativeReactiveEnergyTotal;
    [Required]
    public double AvgNegativeReactiveEnergyTotal { get; set; } = avgNegativeReactiveEnergyTotal;
    [Required]
    public double MedNegativeReactiveEnergyTotal { get; set; } = medNegativeReactiveEnergyTotal;

    #endregion

    #region PositiveActiveEnergyTotal

    [Required]
    public double MinPositiveActiveEnergyTotal { get; set; } = minPositiveActiveEnergyTotal;
    [Required]
    public double MaxPositiveActiveEnergyTotal { get; set; } = maxPositiveActiveEnergyTotal;
    [Required]
    public double AvgPositiveActiveEnergyTotal { get; set; } = avgPositiveActiveEnergyTotal;
    [Required]
    public double MedPositiveActiveEnergyTotal { get; set; } = medPositiveActiveEnergyTotal;

    #endregion

    #region NegativeActiveEnergyTotal

    [Required]
    public double MinNegativeActiveEnergyTotal { get; set; } = minNegativeActiveEnergyTotal;
    [Required]
    public double MaxNegativeActiveEnergyTotal { get; set; } = maxNegativeActiveEnergyTotal;
    [Required]
    public double AvgNegativeActiveEnergyTotal { get; set; } = avgNegativeActiveEnergyTotal;
    [Required]
    public double MedNegativeActiveEnergyTotal { get; set; } = medNegativeActiveEnergyTotal;

    #endregion

    [Required]
    public int AmountOfMeasurements { get; set; } = amountOfMeasurements;

    public static MeasurementAggregatedDto FromAggregatedMeasurement(AggregatedMeasurement measurement)
    {
        return new MeasurementAggregatedDto(
            measurement.Timestamp,
            measurement.MinVoltagePhase1, measurement.MaxVoltagePhase1, measurement.AvgVoltagePhase1, measurement.MedVoltagePhase1,
            measurement.MinVoltagePhase2, measurement.MaxVoltagePhase2, measurement.AvgVoltagePhase2, measurement.MedVoltagePhase2,
            measurement.MinVoltagePhase3, measurement.MaxVoltagePhase3, measurement.AvgVoltagePhase3, measurement.MedVoltagePhase3,
            measurement.MinCurrentPhase1, measurement.MaxCurrentPhase1, measurement.AvgCurrentPhase1, measurement.MedCurrentPhase1,
            measurement.MinCurrentPhase2, measurement.MaxCurrentPhase2, measurement.AvgCurrentPhase2, measurement.MedCurrentPhase2,
            measurement.MinCurrentPhase3, measurement.MaxCurrentPhase3, measurement.AvgCurrentPhase3, measurement.MedCurrentPhase3,
            measurement.MinPositiveActivePower, measurement.MaxPositiveActivePower, measurement.AvgPositiveActivePower, measurement.MedPositiveActivePower,
            measurement.MinNegativeActivePower, measurement.MaxNegativeActivePower, measurement.AvgNegativeActivePower, measurement.MedNegativeActivePower,
            measurement.MinPositiveReactiveEnergyTotal, measurement.MaxPositiveReactiveEnergyTotal, measurement.AvgPositiveReactiveEnergyTotal, measurement.MedPositiveReactiveEnergyTotal,
            measurement.MinNegativeReactiveEnergyTotal, measurement.MaxNegativeReactiveEnergyTotal, measurement.AvgNegativeReactiveEnergyTotal, measurement.MedNegativeReactiveEnergyTotal,
            measurement.MinPositiveActiveEnergyTotal, measurement.MaxPositiveActiveEnergyTotal, measurement.AvgPositiveActiveEnergyTotal, measurement.MedPositiveActiveEnergyTotal,
            measurement.MinNegativeActiveEnergyTotal, measurement.MaxNegativeActiveEnergyTotal, measurement.AvgNegativeActiveEnergyTotal, measurement.MedNegativeActiveEnergyTotal,
            measurement.AmountOfMeasurements
        );
    }
}