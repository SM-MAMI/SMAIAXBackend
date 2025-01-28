using System.ComponentModel.DataAnnotations;

using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.ValueObjects;

namespace SMAIAXBackend.Application.DTOs;

public class SmartMeterOverviewDto(Guid id, Guid connectorSerialNumber, string name, int metadataCount, int policyCount)
{
    [Required]
    public Guid Id { get; set; } = id;
    [Required] 
    public Guid ConnectorSerialNumber { get; set; } = connectorSerialNumber;
    [Required]
    public string Name { get; set; } = name;
    [Required]
    public int MetadataCount { get; set; } = metadataCount;
    [Required]
    public int PolicyCount { get; set; } = policyCount;

    public static SmartMeterOverviewDto FromSmartMeter(SmartMeter smartMeter, List<Policy> policies)
    {
        return new SmartMeterOverviewDto(smartMeter.Id.Id, smartMeter.ConnectorSerialNumber.SerialNumber, smartMeter.Name,
            smartMeter.Metadata.Count, policies.Count);
    }
}