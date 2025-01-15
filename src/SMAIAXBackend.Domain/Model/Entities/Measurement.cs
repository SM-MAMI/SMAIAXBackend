using SMAIAXBackend.Domain.Model.ValueObjects.Ids;

namespace SMAIAXBackend.Domain.Model.Entities;

public sealed class Measurement
{
    public double PositiveActivePower { get; }
    public double PositiveActiveEnergyTotal { get; }
    public double NegativeActivePower { get; }
    public double NegativeActiveEnergyTotal { get; }
    public double ReactiveEnergyQuadrant1Total { get; }
    public double ReactiveEnergyQuadrant3Total { get; }
    public double TotalPower { get; }
    public double CurrentPhase1 { get; }
    public double VoltagePhase1 { get; }
    public double CurrentPhase2 { get; }
    public double VoltagePhase2 { get; }
    public double CurrentPhase3 { get; }
    public double VoltagePhase3 { get; }
    public string Uptime { get; }
    public DateTime Timestamp { get; }
    public SmartMeterId SmartMeterId { get; }
}