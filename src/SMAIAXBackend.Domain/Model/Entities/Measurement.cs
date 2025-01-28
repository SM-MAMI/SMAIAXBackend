using SMAIAXBackend.Domain.Model.ValueObjects.Ids;

namespace SMAIAXBackend.Domain.Model.Entities;

public sealed class Measurement
{
    public SmartMeterId SmartMeterId { get; }
    public DateTime Timestamp { get; }
    public double VoltagePhase1 { get; }
    public double VoltagePhase2 { get; }
    public double VoltagePhase3 { get; }
    public double CurrentPhase1 { get; }
    public double CurrentPhase2 { get; }
    public double CurrentPhase3 { get; }
    public double PositiveActivePower { get; }
    public double NegativeActivePower { get; }
    public double PositiveReactiveEnergyTotal { get; }
    public double NegativeReactiveEnergyTotal { get; }
    public double PositiveActiveEnergyTotal { get; }
    public double NegativeActiveEnergyTotal { get; }
}