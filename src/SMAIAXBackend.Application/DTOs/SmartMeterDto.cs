using System.ComponentModel.DataAnnotations;

using SMAIAXBackend.Domain.Model.Entities;

namespace SMAIAXBackend.Application.DTOs;

public class SmartMeterDto(Guid id, Guid connectorSerialNumber, string name, List<MetadataDto> metadata)
{
    [Required]
    public Guid Id { get; set; } = id;
    [Required]
    public Guid ConnectorSerialNumber { get; set; } = connectorSerialNumber;
    [Required]
    public string Name { get; set; } = name;
    [Required]
    public List<MetadataDto> Metadata { get; set; } = metadata;

    public static SmartMeterDto FromSmartMeter(SmartMeter smartMeter)
    {
        return new SmartMeterDto(smartMeter.Id.Id, smartMeter.ConnectorSerialNumber.SerialNumber, smartMeter.Name, smartMeter.Metadata.Select(MetadataDto.FromMetadata).ToList());
    }
}