using System.ComponentModel.DataAnnotations;

namespace SMAIAXBackend.Application.DTOs;

public class SmartMeterAssignDto(Guid serialNumber, string name, MetadataCreateDto? metadata)
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Serial number is required")]
    public Guid SerialNumber { get; set; } = serialNumber;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Smart meter name is required")]
    public string Name { get; set; } = name;

    public MetadataCreateDto? Metadata { get; set; } = metadata;
}