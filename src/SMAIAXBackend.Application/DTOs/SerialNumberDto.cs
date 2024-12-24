using System.ComponentModel.DataAnnotations;

namespace SMAIAXBackend.Application.DTOs;

public class SerialNumberDto(Guid serialNumber)
{
    [Required]
    public Guid SerialNumber { get; set; } = serialNumber;
}