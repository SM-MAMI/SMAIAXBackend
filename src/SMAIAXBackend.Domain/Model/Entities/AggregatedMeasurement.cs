namespace SMAIAXBackend.Domain.Model.Entities;

public sealed class AggregatedMeasurement(
    string uptime,
    DateTime timestamp,
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
    public string Uptime { get; } = uptime;
    public DateTime Timestamp { get; } = timestamp;

    // Positive Active Power Section
    public double MinPositiveActivePower { get; } = minPositiveActivePower;
    public double MaxPositiveActivePower { get; } = maxPositiveActivePower;
    public double AvgPositiveActivePower { get; } = avgPositiveActivePower;
    public double MedPositiveActivePower { get; } = medPositiveActivePower;

    // Positive Active Energy Total Section
    public double MinPositiveActiveEnergyTotal { get; } = minPositiveActiveEnergyTotal;
    public double MaxPositiveActiveEnergyTotal { get; } = maxPositiveActiveEnergyTotal;
    public double AvgPositiveActiveEnergyTotal { get; } = avgPositiveActiveEnergyTotal;
    public double MedPositiveActiveEnergyTotal { get; } = medPositiveActiveEnergyTotal;

    // Negative Active Power Section
    public double MinNegativeActivePower { get; } = minNegativeActivePower;
    public double MaxNegativeActivePower { get; } = maxNegativeActivePower;
    public double AvgNegativeActivePower { get; } = avgNegativeActivePower;
    public double MedNegativeActivePower { get; } = medNegativeActivePower;

    // Negative Active Energy Total Section
    public double MinNegativeActiveEnergyTotal { get; } = minNegativeActiveEnergyTotal;
    public double MaxNegativeActiveEnergyTotal { get; } = maxNegativeActiveEnergyTotal;
    public double AvgNegativeActiveEnergyTotal { get; } = avgNegativeActiveEnergyTotal;
    public double MedNegativeActiveEnergyTotal { get; } = medNegativeActiveEnergyTotal;

    // Reactive Energy Quadrant 1 Section
    public double MinReactiveEnergyQuadrant1Total { get; } = minReactiveEnergyQuadrant1Total;
    public double MaxReactiveEnergyQuadrant1Total { get; } = maxReactiveEnergyQuadrant1Total;
    public double AvgReactiveEnergyQuadrant1Total { get; } = avgReactiveEnergyQuadrant1Total;
    public double MedReactiveEnergyQuadrant1Total { get; } = medReactiveEnergyQuadrant1Total;

    // Reactive Energy Quadrant 3 Section
    public double MinReactiveEnergyQuadrant3Total { get; } = minReactiveEnergyQuadrant3Total;
    public double MaxReactiveEnergyQuadrant3Total { get; } = maxReactiveEnergyQuadrant3Total;
    public double AvgReactiveEnergyQuadrant3Total { get; } = avgReactiveEnergyQuadrant3Total;
    public double MedReactiveEnergyQuadrant3Total { get; } = medReactiveEnergyQuadrant3Total;

    // Total Power Section
    public double MinTotalPower { get; } = minTotalPower;
    public double MaxTotalPower { get; } = maxTotalPower;
    public double AvgTotalPower { get; } = avgTotalPower;
    public double MedTotalPower { get; } = medTotalPower;

    // Current Phase 1 Section
    public double MinCurrentPhase1 { get; } = minCurrentPhase1;
    public double MaxCurrentPhase1 { get; } = maxCurrentPhase1;
    public double AvgCurrentPhase1 { get; } = avgCurrentPhase1;
    public double MedCurrentPhase1 { get; } = medCurrentPhase1;

    // Voltage Phase 1 Section
    public double MinVoltagePhase1 { get; } = minVoltagePhase1;
    public double MaxVoltagePhase1 { get; } = maxVoltagePhase1;
    public double AvgVoltagePhase1 { get; } = avgVoltagePhase1;
    public double MedVoltagePhase1 { get; } = medVoltagePhase1;

    // Current Phase 2 Section
    public double MinCurrentPhase2 { get; } = minCurrentPhase2;
    public double MaxCurrentPhase2 { get; } = maxCurrentPhase2;
    public double AvgCurrentPhase2 { get; } = avgCurrentPhase2;
    public double MedCurrentPhase2 { get; } = medCurrentPhase2;

    // Voltage Phase 2 Section
    public double MinVoltagePhase2 { get; } = minVoltagePhase2;
    public double MaxVoltagePhase2 { get; } = maxVoltagePhase2;
    public double AvgVoltagePhase2 { get; } = avgVoltagePhase2;
    public double MedVoltagePhase2 { get; } = medVoltagePhase2;

    // Current Phase 3 Section
    public double MinCurrentPhase3 { get; } = minCurrentPhase3;
    public double MaxCurrentPhase3 { get; } = maxCurrentPhase3;
    public double AvgCurrentPhase3 { get; } = avgCurrentPhase3;
    public double MedCurrentPhase3 { get; } = medCurrentPhase3;

    // Voltage Phase 3 Section
    public double MinVoltagePhase3 { get; } = minVoltagePhase3;
    public double MaxVoltagePhase3 { get; } = maxVoltagePhase3;
    public double AvgVoltagePhase3 { get; } = avgVoltagePhase3;
    public double MedVoltagePhase3 { get; } = medVoltagePhase3;

    public int AmountOfMeasurements { get; } = amountOfMeasurements;
}