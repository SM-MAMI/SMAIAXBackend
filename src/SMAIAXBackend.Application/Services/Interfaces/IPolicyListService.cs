using SMAIAXBackend.Application.DTOs;
using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.Enums;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;

namespace SMAIAXBackend.Application.Services.Interfaces;

public interface IPolicyListService
{
    Task<List<PolicyDto>> GetPoliciesBySmartMeterIdAsync(SmartMeterId smartMeterId);
    Task<List<PolicyDto>> GetPoliciesAsync();
    Task<PolicyDto> GetPolicyByIdAsync(PolicyId policyId);

    Task<List<PolicyDto>> GetFilteredPoliciesAsync(decimal? maxPrice, MeasurementResolution? measurementResolution,
        LocationResolution? locationResolution);

    Task<MeasurementListDto> GetMeasurementsByPolicyIdAsync(Guid policyId);

    Task<int> GetMeasurementCountByTenantAndPolicyAsync(Policy policy, Tenant? tenant = null);
}