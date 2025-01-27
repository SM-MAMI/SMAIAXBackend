using System.ComponentModel.DataAnnotations;

using SMAIAXBackend.Domain.Model.Entities;

namespace SMAIAXBackend.Application.DTOs;

public class ContractDto(
    Guid id,
    DateTime createdAt,
    PolicyDto policy,
    MeasurementListDto measurementList)
{
    [Required] public Guid Id { get; set; } = id;
    [Required] public DateTime CreatedAt { get; set; } = createdAt;
    [Required] public PolicyDto Policy { get; set; } = policy;
    [Required] public MeasurementListDto MeasurementList { get; set; } = measurementList;

    public static ContractDto FromContract(Contract contract, PolicyDto policy, MeasurementListDto measurementList)
    {
        return new ContractDto(contract.Id.Id, contract.CreatedAt, policy, measurementList);
    }
}