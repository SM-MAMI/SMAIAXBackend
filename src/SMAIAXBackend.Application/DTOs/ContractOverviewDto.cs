using System.ComponentModel.DataAnnotations;

using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.Enums;

namespace SMAIAXBackend.Application.DTOs;

public class ContractOverviewDto(
    Guid id,
    DateTime createdAt,
    string policyName,
    MeasurementResolution measurementResolution,
    LocationResolution locationResolution)
{
    [Required] public Guid Id { get; set; } = id;
    [Required] public DateTime CreatedAt { get; set; } = createdAt;
    [Required] public string PolicyName { get; set; } = policyName;
    [Required] public MeasurementResolution MeasurementResolution { get; set; } = measurementResolution;
    [Required] public LocationResolution LocationResolution { get; set; } = locationResolution;

    public static ContractOverviewDto FromContract(Contract contract, Policy policy)
    {
        return new ContractOverviewDto(contract.Id.Id, contract.CreatedAt, policy.Name, policy.MeasurementResolution,
            policy.LocationResolution);
    }
}