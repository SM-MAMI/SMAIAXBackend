namespace SMAIAXBackend.Domain.Model.Entities;

public sealed class AggregatedMeasurement(
    DateTime timestamp,
    int amountOfMeasurements,
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
    double medNegativeActiveEnergyTotal)
{
    public DateTime Timestamp { get; } = timestamp;

    #region Voltage1

    public double MinVoltagePhase1 { get; } = minVoltagePhase1;
    public double MaxVoltagePhase1 { get; } = maxVoltagePhase1;
    public double AvgVoltagePhase1 { get; } = avgVoltagePhase1;
    public double MedVoltagePhase1 { get; } = medVoltagePhase1;

    #endregion

    #region Voltage2

    public double MinVoltagePhase2 { get; } = minVoltagePhase2;
    public double MaxVoltagePhase2 { get; } = maxVoltagePhase2;
    public double AvgVoltagePhase2 { get; } = avgVoltagePhase2;
    public double MedVoltagePhase2 { get; } = medVoltagePhase2;

    #endregion

    #region Voltage3

    public double MinVoltagePhase3 { get; } = minVoltagePhase3;
    public double MaxVoltagePhase3 { get; } = maxVoltagePhase3;
    public double AvgVoltagePhase3 { get; } = avgVoltagePhase3;
    public double MedVoltagePhase3 { get; } = medVoltagePhase3;

    #endregion

    #region CurrentPhase1

    public double MinCurrentPhase1 { get; } = minCurrentPhase1;
    public double MaxCurrentPhase1 { get; } = maxCurrentPhase1;
    public double AvgCurrentPhase1 { get; } = avgCurrentPhase1;
    public double MedCurrentPhase1 { get; } = medCurrentPhase1;

    #endregion

    #region CurrentPhase2

    public double MinCurrentPhase2 { get; } = minCurrentPhase2;
    public double MaxCurrentPhase2 { get; } = maxCurrentPhase2;
    public double AvgCurrentPhase2 { get; } = avgCurrentPhase2;
    public double MedCurrentPhase2 { get; } = medCurrentPhase2;

    #endregion

    #region CurrentPhase3

    public double MinCurrentPhase3 { get; } = minCurrentPhase3;
    public double MaxCurrentPhase3 { get; } = maxCurrentPhase3;
    public double AvgCurrentPhase3 { get; } = avgCurrentPhase3;
    public double MedCurrentPhase3 { get; } = medCurrentPhase3;

    #endregion

    #region PositiveActivePower

    public double MinPositiveActivePower { get; } = minPositiveActivePower;
    public double MaxPositiveActivePower { get; } = maxPositiveActivePower;
    public double AvgPositiveActivePower { get; } = avgPositiveActivePower;
    public double MedPositiveActivePower { get; } = medPositiveActivePower;

    #endregion

    #region NegativeActivePower

    public double MinNegativeActivePower { get; } = minNegativeActivePower;
    public double MaxNegativeActivePower { get; } = maxNegativeActivePower;
    public double AvgNegativeActivePower { get; } = avgNegativeActivePower;
    public double MedNegativeActivePower { get; } = medNegativeActivePower;

    #endregion

    #region PositiveReactiveEnergyTotal

    public double MinPositiveReactiveEnergyTotal { get; } = minPositiveReactiveEnergyTotal;
    public double MaxPositiveReactiveEnergyTotal { get; } = maxPositiveReactiveEnergyTotal;
    public double AvgPositiveReactiveEnergyTotal { get; } = avgPositiveReactiveEnergyTotal;
    public double MedPositiveReactiveEnergyTotal { get; } = medPositiveReactiveEnergyTotal;

    #endregion

    #region NegativeReactiveEnergyTotal

    public double MinNegativeReactiveEnergyTotal { get; } = minNegativeReactiveEnergyTotal;
    public double MaxNegativeReactiveEnergyTotal { get; } = maxNegativeReactiveEnergyTotal;
    public double AvgNegativeReactiveEnergyTotal { get; } = avgNegativeReactiveEnergyTotal;
    public double MedNegativeReactiveEnergyTotal { get; } = medNegativeReactiveEnergyTotal;

    #endregion

    #region PositiveActiveEnergyTotal

    public double MinPositiveActiveEnergyTotal { get; } = minPositiveActiveEnergyTotal;
    public double MaxPositiveActiveEnergyTotal { get; } = maxPositiveActiveEnergyTotal;
    public double AvgPositiveActiveEnergyTotal { get; } = avgPositiveActiveEnergyTotal;
    public double MedPositiveActiveEnergyTotal { get; } = medPositiveActiveEnergyTotal;

    #endregion

    #region NegativeActiveEnergyTotal

    public double MinNegativeActiveEnergyTotal { get; } = minNegativeActiveEnergyTotal;
    public double MaxNegativeActiveEnergyTotal { get; } = maxNegativeActiveEnergyTotal;
    public double AvgNegativeActiveEnergyTotal { get; } = avgNegativeActiveEnergyTotal;
    public double MedNegativeActiveEnergyTotal { get; } = medNegativeActiveEnergyTotal;

    #endregion

    public int AmountOfMeasurements { get; } = amountOfMeasurements;
}