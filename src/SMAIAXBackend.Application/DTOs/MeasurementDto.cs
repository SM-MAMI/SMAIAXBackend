using SMAIAXBackend.Domain.Model.Entities;

namespace SMAIAXBackend.Application.DTOs;

public class MeasurementDto(
    DateTime timestamp,
    string uptime)
{
    public MeasurementDto(DateTime timestamp, string uptime, double? positiveActivePower,
        double? positiveActiveEnergyTotal, double? negativeActivePower, double? negativeActiveEnergyTotal,
        double? reactiveEnergyQuadrant1Total, double? reactiveEnergyQuadrant3Total, double? totalPower,
        double? currentPhase1, double? voltagePhase1, double? currentPhase2, double? voltagePhase2,
        double? currentPhase3,
        double? voltagePhase3) : this(timestamp, uptime)
    {
        PositiveActivePower = positiveActivePower;
        PositiveActiveEnergyTotal = positiveActiveEnergyTotal;
        NegativeActivePower = negativeActivePower;
        NegativeActiveEnergyTotal = negativeActiveEnergyTotal;
        ReactiveEnergyQuadrant1Total = reactiveEnergyQuadrant1Total;
        ReactiveEnergyQuadrant3Total = reactiveEnergyQuadrant3Total;
        TotalPower = totalPower;
        CurrentPhase1 = currentPhase1;
        VoltagePhase1 = voltagePhase1;
        CurrentPhase2 = currentPhase2;
        VoltagePhase2 = voltagePhase2;
        CurrentPhase3 = currentPhase3;
        VoltagePhase3 = voltagePhase3;
    }

    public MeasurementDto(DateTime timestamp, string uptime, double? minPositiveActivePower,
        double? maxPositiveActivePower, double? avgPositiveActivePower, double? medPositiveActivePower,
        double? minPositiveActiveEnergyTotal, double? maxPositiveActiveEnergyTotal,
        double? avgPositiveActiveEnergyTotal, double? medPositiveActiveEnergyTotal,
        double? minNegativeActivePower, double? maxNegativeActivePower, double? avgNegativeActivePower,
        double? medNegativeActivePower, double? minNegativeActiveEnergyTotal,
        double? maxNegativeActiveEnergyTotal, double? avgNegativeActiveEnergyTotal,
        double? medNegativeActiveEnergyTotal, double? minReactiveEnergyQuadrant1Total,
        double? maxReactiveEnergyQuadrant1Total, double? avgReactiveEnergyQuadrant1Total,
        double? medReactiveEnergyQuadrant1Total,
        double? minReactiveEnergyQuadrant3Total, double? maxReactiveEnergyQuadrant3Total,
        double? avgReactiveEnergyQuadrant3Total, double? medReactiveEnergyQuadrant3Total, double? minTotalPower,
        double? maxTotalPower, double? avgTotalPower,
        double? medTotalPower, double? minCurrentPhase1, double? maxCurrentPhase1,
        double? avgCurrentPhase1, double? medCurrentPhase1, double? minVoltagePhase1,
        double? maxVoltagePhase1, double? avgVoltagePhase1, double? medVoltagePhase1,
        double? minCurrentPhase2, double? maxCurrentPhase2, double? avgCurrentPhase2, double? medCurrentPhase2,
        double? minVoltagePhase2, double? maxVoltagePhase2, double? avgVoltagePhase2,
        double? medVoltagePhase2, double? minCurrentPhase3, double? maxCurrentPhase3,
        double? avgCurrentPhase3, double? medCurrentPhase3, double? minVoltagePhase3,
        double? maxVoltagePhase3, double? avgVoltagePhase3, double? medVoltagePhase3,
        int? amountOfMeasurements) : this(timestamp, uptime)
    {
        MinPositiveActivePower = minPositiveActivePower;
        MaxPositiveActivePower = maxPositiveActivePower;
        AvgPositiveActivePower = avgPositiveActivePower;
        MedPositiveActivePower = medPositiveActivePower;
        MinPositiveActiveEnergyTotal = minPositiveActiveEnergyTotal;
        MaxPositiveActiveEnergyTotal = maxPositiveActiveEnergyTotal;
        AvgPositiveActiveEnergyTotal = avgPositiveActiveEnergyTotal;
        MedPositiveActiveEnergyTotal = medPositiveActiveEnergyTotal;
        MinNegativeActivePower = minNegativeActivePower;
        MaxNegativeActivePower = maxNegativeActivePower;
        AvgNegativeActivePower = avgNegativeActivePower;
        MedNegativeActivePower = medNegativeActivePower;
        MinNegativeActiveEnergyTotal = minNegativeActiveEnergyTotal;
        MaxNegativeActiveEnergyTotal = maxNegativeActiveEnergyTotal;
        AvgNegativeActiveEnergyTotal = avgNegativeActiveEnergyTotal;
        MedNegativeActiveEnergyTotal = medNegativeActiveEnergyTotal;
        MinReactiveEnergyQuadrant1Total = minReactiveEnergyQuadrant1Total;
        MaxReactiveEnergyQuadrant1Total = maxReactiveEnergyQuadrant1Total;
        AvgReactiveEnergyQuadrant1Total = avgReactiveEnergyQuadrant1Total;
        MedReactiveEnergyQuadrant1Total = medReactiveEnergyQuadrant1Total;
        MinReactiveEnergyQuadrant3Total = minReactiveEnergyQuadrant3Total;
        MaxReactiveEnergyQuadrant3Total = maxReactiveEnergyQuadrant3Total;
        AvgReactiveEnergyQuadrant3Total = avgReactiveEnergyQuadrant3Total;
        MedReactiveEnergyQuadrant3Total = medReactiveEnergyQuadrant3Total;
        MinTotalPower = minTotalPower;
        MaxTotalPower = maxTotalPower;
        AvgTotalPower = avgTotalPower;
        MedTotalPower = medTotalPower;
        MinCurrentPhase1 = minCurrentPhase1;
        MaxCurrentPhase1 = maxCurrentPhase1;
        AvgCurrentPhase1 = avgCurrentPhase1;
        MedCurrentPhase1 = medCurrentPhase1;
        MinVoltagePhase1 = minVoltagePhase1;
        MaxVoltagePhase1 = maxVoltagePhase1;
        AvgVoltagePhase1 = avgVoltagePhase1;
        MedVoltagePhase1 = medVoltagePhase1;
        MinCurrentPhase2 = minCurrentPhase2;
        MaxCurrentPhase2 = maxCurrentPhase2;
        AvgCurrentPhase2 = avgCurrentPhase2;
        MedCurrentPhase2 = medCurrentPhase2;
        MinVoltagePhase2 = minVoltagePhase2;
        MaxVoltagePhase2 = maxVoltagePhase2;
        AvgVoltagePhase2 = avgVoltagePhase2;
        MedVoltagePhase2 = medVoltagePhase2;
        MinCurrentPhase3 = minCurrentPhase3;
        MaxCurrentPhase3 = maxCurrentPhase3;
        AvgCurrentPhase3 = avgCurrentPhase3;
        MedCurrentPhase3 = medCurrentPhase3;
        MinVoltagePhase3 = minVoltagePhase3;
        MaxVoltagePhase3 = maxVoltagePhase3;
        AvgVoltagePhase3 = avgVoltagePhase3;
        MedVoltagePhase3 = medVoltagePhase3;
        AmountOfMeasurements = amountOfMeasurements;
    }

    public string Uptime { get; set; } = uptime;
    public DateTime Timestamp { get; set; } = timestamp;
    public double? PositiveActivePower { get; set; }
    public double? PositiveActiveEnergyTotal { get; set; }
    public double? NegativeActivePower { get; set; }
    public double? NegativeActiveEnergyTotal { get; set; }
    public double? ReactiveEnergyQuadrant1Total { get; set; }
    public double? ReactiveEnergyQuadrant3Total { get; set; }
    public double? TotalPower { get; set; }
    public double? CurrentPhase1 { get; set; }
    public double? VoltagePhase1 { get; set; }
    public double? CurrentPhase2 { get; set; }
    public double? VoltagePhase2 { get; set; }
    public double? CurrentPhase3 { get; set; }
    public double? VoltagePhase3 { get; set; }

    // Extended properties for Min, Max, Avg, Med, and Count
    public double? MinPositiveActivePower { get; set; }
    public double? MaxPositiveActivePower { get; set; }
    public double? AvgPositiveActivePower { get; set; }
    public double? MedPositiveActivePower { get; set; }

    public double? MinPositiveActiveEnergyTotal { get; set; }
    public double? MaxPositiveActiveEnergyTotal { get; set; }
    public double? AvgPositiveActiveEnergyTotal { get; set; }
    public double? MedPositiveActiveEnergyTotal { get; set; }

    public double? MinNegativeActivePower { get; set; }
    public double? MaxNegativeActivePower { get; set; }
    public double? AvgNegativeActivePower { get; set; }
    public double? MedNegativeActivePower { get; set; }

    public double? MinNegativeActiveEnergyTotal { get; set; }
    public double? MaxNegativeActiveEnergyTotal { get; set; }
    public double? AvgNegativeActiveEnergyTotal { get; set; }
    public double? MedNegativeActiveEnergyTotal { get; set; }

    public double? MinReactiveEnergyQuadrant1Total { get; set; }
    public double? MaxReactiveEnergyQuadrant1Total { get; set; }
    public double? AvgReactiveEnergyQuadrant1Total { get; set; }
    public double? MedReactiveEnergyQuadrant1Total { get; set; }

    public double? MinReactiveEnergyQuadrant3Total { get; set; }
    public double? MaxReactiveEnergyQuadrant3Total { get; set; }
    public double? AvgReactiveEnergyQuadrant3Total { get; set; }
    public double? MedReactiveEnergyQuadrant3Total { get; set; }

    public double? MinTotalPower { get; set; }
    public double? MaxTotalPower { get; set; }
    public double? AvgTotalPower { get; set; }
    public double? MedTotalPower { get; set; }

    public double? MinCurrentPhase1 { get; set; }
    public double? MaxCurrentPhase1 { get; set; }
    public double? AvgCurrentPhase1 { get; set; }
    public double? MedCurrentPhase1 { get; set; }

    public double? MinVoltagePhase1 { get; set; }
    public double? MaxVoltagePhase1 { get; set; }
    public double? AvgVoltagePhase1 { get; set; }
    public double? MedVoltagePhase1 { get; set; }

    public double? MinCurrentPhase2 { get; set; }
    public double? MaxCurrentPhase2 { get; set; }
    public double? AvgCurrentPhase2 { get; set; }
    public double? MedCurrentPhase2 { get; set; }

    public double? MinVoltagePhase2 { get; set; }
    public double? MaxVoltagePhase2 { get; set; }
    public double? AvgVoltagePhase2 { get; set; }
    public double? MedVoltagePhase2 { get; set; }

    public double? MinCurrentPhase3 { get; set; }
    public double? MaxCurrentPhase3 { get; set; }
    public double? AvgCurrentPhase3 { get; set; }
    public double? MedCurrentPhase3 { get; set; }

    public double? MinVoltagePhase3 { get; set; }
    public double? MaxVoltagePhase3 { get; set; }
    public double? AvgVoltagePhase3 { get; set; }
    public double? MedVoltagePhase3 { get; set; }

    public int? AmountOfMeasurements { get; set; }

    public static MeasurementDto FromMeasurement(Measurement measurement)
    {
        return new MeasurementDto(
            measurement.Timestamp, measurement.Uptime,
            measurement.PositiveActivePower, measurement.PositiveActiveEnergyTotal,
            measurement.NegativeActivePower, measurement.NegativeActiveEnergyTotal,
            measurement.ReactiveEnergyQuadrant1Total, measurement.ReactiveEnergyQuadrant3Total, measurement.TotalPower,
            measurement.CurrentPhase1, measurement.VoltagePhase1,
            measurement.CurrentPhase2, measurement.VoltagePhase2, measurement.CurrentPhase3, measurement.VoltagePhase3);
    }

    public static MeasurementDto FromAggregateMeasurement(AggregatedMeasurement measurement)
    {
        return new MeasurementDto(
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