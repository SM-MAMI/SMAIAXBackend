using System.ComponentModel.DataAnnotations;

namespace SMAIAXBackend.Application.DTOs;

public class ContractCreateDto(Guid policyId, Guid userId)
{
    [Required] public Guid PolicyId { get; set; } = policyId;
    [Required] public Guid UserId { get; set; } = userId;
}