using System.ComponentModel.DataAnnotations;

using SMAIAXBackend.Domain.Model.Entities;

namespace SMAIAXBackend.Application.DTOs;

public class ContractDto(
    Guid id,
    DateTime createdAt,
    PolicyDto policy)
{
    [Required] public Guid Id { get; set; } = id;
    [Required] public DateTime CreatedAt { get; set; } = createdAt;
    [Required] public PolicyDto Policy { get; set; } = policy;

    public static ContractDto FromContract(Contract contract, PolicyDto policy)
    {
        return new ContractDto(contract.Id.Id, contract.CreatedAt, policy);
    }
}