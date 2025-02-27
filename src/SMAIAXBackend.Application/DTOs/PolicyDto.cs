using System.ComponentModel.DataAnnotations;

using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.Enums;

namespace SMAIAXBackend.Application.DTOs;

public class PolicyDto(
    Guid id,
    string name,
    MeasurementResolution measurementResolution,
    LocationResolution locationResolution,
    decimal price,
    int measurementCount)
{
    [Required] public Guid Id { get; set; } = id;
    [Required(AllowEmptyStrings = false)] public string Name { get; set; } = name;
    [Required] public MeasurementResolution MeasurementResolution { get; set; } = measurementResolution;
    [Required] public LocationResolution LocationResolution { get; set; } = locationResolution;
    [Required] public decimal Price { get; set; } = price;
    public int MeasurementCount { get; set; } = measurementCount;

    public static PolicyDto FromPolicy(Policy policy, int measurementCount)
    {
        return new PolicyDto(policy.Id.Id, policy.Name, policy.MeasurementResolution, policy.LocationResolution,
            policy.Price, measurementCount);
    }
}