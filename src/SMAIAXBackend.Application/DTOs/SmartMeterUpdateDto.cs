using System.ComponentModel.DataAnnotations;

namespace SMAIAXBackend.Application.DTOs;

public class SmartMeterUpdateDto(Guid id, string name)
{
    [Required]
    public Guid Id { get; set; } = id;
    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; } = name;
}