using System.ComponentModel.DataAnnotations;

namespace SMAIAXBackend.Application.DTOs;

public class NameDto(string firstName, string lastName)
{
    [Required(AllowEmptyStrings = false)]
    public string FirstName { get; set; } = firstName;

    [Required(AllowEmptyStrings = false)]
    public string LastName { get; set; } = lastName;
}