using System.ComponentModel.DataAnnotations;

using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.Enums;

namespace SMAIAXBackend.Application.DTOs;

public class ContractOverviewDto(
    Guid id,
    DateTime createdAt,
    PolicyDto policyDto)
{
    [Required] public Guid Id { get; set; } = id;
    [Required] public DateTime CreatedAt { get; set; } = createdAt;
    [Required] public PolicyDto PolicyDto { get; set; } = policyDto;

    public static ContractOverviewDto FromContract(Contract contract, Policy policy, int measurementCount)
    {
        var policyDto = PolicyDto.FromPolicy(policy, measurementCount);
        return new ContractOverviewDto(contract.Id.Id, contract.CreatedAt, policyDto);
    }
}