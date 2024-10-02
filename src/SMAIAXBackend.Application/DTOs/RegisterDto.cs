using System.ComponentModel.DataAnnotations;
using SMAIAXBackend.Domain.Model.ValueObjects;

namespace SMAIAXBackend.Application.DTOs;

public class RegisterDto(
    string email,
    string password,
    Name name,
    Address address)
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = email;

    [Required]
    public string Password { get; set; } = password;

    [Required]
    public Name Name { get; set; } = name;

    [Required]
    public Address Address { get; set; } = address;
}