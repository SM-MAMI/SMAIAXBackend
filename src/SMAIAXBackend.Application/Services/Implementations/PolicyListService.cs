using Microsoft.Extensions.Logging;

using SMAIAXBackend.Application.DTOs;
using SMAIAXBackend.Application.Exceptions;
using SMAIAXBackend.Application.Services.Interfaces;
using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.Enums;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;
using SMAIAXBackend.Domain.Repositories;
using SMAIAXBackend.Domain.Specifications;

namespace SMAIAXBackend.Application.Services.Implementations;

public class PolicyListService(
    ITenantRepository tenantRepository,
    ISmartMeterRepository smartMeterRepository,
    IMeasurementRepository measurementRepository,
    IPolicyRepository policyRepository,
    IMeasurementListService measurementListService,
    ITenantContextService tenantContextService,
    ILogger<PolicyListService> logger) : IPolicyListService
{
    public async Task<List<PolicyDto>> GetPoliciesBySmartMeterIdAsync(SmartMeterId smartMeterId)
    {
        var policies = await policyRepository.GetPoliciesBySmartMeterIdAsync(smartMeterId);
        return await ToPolicyDtoListWithMeasurementCount(policies);
    }

    public async Task<List<PolicyDto>> GetPoliciesAsync()
    {
        var policies = await policyRepository.GetPoliciesAsync();
        return await ToPolicyDtoListWithMeasurementCount(policies);
    }

    public async Task<List<PolicyDto>> GetFilteredPoliciesAsync(decimal? maxPrice,
        MeasurementResolution? measurementResolution, LocationResolution? locationResolution)
    {
        ISpecification<Policy> specification = new BaseSpecification<Policy>();

        if (maxPrice.HasValue)
        {
            var priceSpecification = new PriceSpecification(maxPrice.Value);
            specification = new AndSpecification<Policy>(specification, priceSpecification);
        }

        if (measurementResolution.HasValue)
        {
            var measurementResolutionSpecification =
                new MeasurementResolutionSpecification(measurementResolution.Value);
            specification = new AndSpecification<Policy>(specification, measurementResolutionSpecification);
        }

        if (locationResolution.HasValue)
        {
            var locationResolutionSpecification =
                new PolicyLocationResolutionSpecification(locationResolution.Value);
            specification = new AndSpecification<Policy>(specification, locationResolutionSpecification);
        }

        var currentTenant = await tenantContextService.GetCurrentTenantAsync();
        var tenants = await tenantRepository.GetAllAsync();
        var matchingPolicies = new List<PolicyDto>();

        foreach (var tenant in tenants.Where(t => !t.Equals(currentTenant)))
        {
            var policies = await policyRepository.GetPoliciesByTenantAsync(tenant);
            var filteredPolicies = policies.Where(policy => specification.IsSatisfiedBy(policy)).ToList();
            foreach (var policy in filteredPolicies)
            {
                var measurementCount = await GetMeasurementCountByTenantAndPolicyAsync(policy, tenant);
                matchingPolicies.Add(PolicyDto.FromPolicy(policy, measurementCount));
            }
        }

        return matchingPolicies;
    }

    public async Task<MeasurementListDto> GetMeasurementsByPolicyIdAsync(Guid policyId)
    {
        var policy = await policyRepository.GetPolicyByIdAsync(new PolicyId(policyId));
        if (policy == null)
        {
            logger.LogError("Policy with id '{policyId}' not found.", policyId);
            throw new PolicyNotFoundException(policyId);
        }

        if (policy.LocationResolution == LocationResolution.None)
        {
            // If location resolution "does not matter", return all measurements.
            return await measurementListService.GetMeasurementsBySmartMeterAndResolutionAsync(policy.SmartMeterId.Id,
                policy.MeasurementResolution);
        }

        // Otherwise location resolution must match with metadata.
        var smartMeter = await smartMeterRepository.GetSmartMeterByIdAsync(policy.SmartMeterId);
        if (smartMeter == null)
        {
            throw new SmartMeterNotFoundException(policy.SmartMeterId);
        }

        var metadata = smartMeter.Metadata.OrderBy(m => m.ValidFrom).ToList();
        if (metadata.Count == 0)
        {
            // If no metadata given, policy location resolution can not match because there is no location reference for the measurements.
            return policy.MeasurementResolution != MeasurementResolution.Raw
                ? new MeasurementListDto(null, [], 0)
                : new MeasurementListDto([], null, 0);
        }

        var timeSpans = new List<(DateTime?, DateTime?)>();
        var specification = new MetadataLocationResolutionSpecification(policy.LocationResolution);
        for (var i = 0; i < metadata!.Count; i += 1)
        {
            if (!specification.IsSatisfiedBy(metadata[i]))
            {
                continue;
            }

            var nextIndex = i + 1;
            timeSpans.Add((metadata[i].ValidFrom, nextIndex >= metadata!.Count ? null : metadata[i + 1].ValidFrom));
        }

        return await measurementListService.GetMeasurementsBySmartMeterAndResolutionAsync(smartMeter.Id.Id,
            policy.MeasurementResolution, timeSpans);
    }

    public async Task<int> GetMeasurementCountByTenantAndPolicyAsync(Policy policy, Tenant? tenant = null)
    {
        if (policy.LocationResolution == LocationResolution.None)
        {
            // If location resolution "does not matter", filter for all measurements.
            return await measurementRepository.GetMeasurementCountBySmartMeterAndResolution(policy.SmartMeterId,
                policy.MeasurementResolution, null, null, tenant);
        }

        // Otherwise location resolution must match with metadata.
        var smartMeter = tenant != null
            ? await smartMeterRepository.GetSmartMeterByTenantAndIdAsync(tenant, policy.SmartMeterId)
            : await smartMeterRepository.GetSmartMeterByIdAsync(policy.SmartMeterId);
        if (smartMeter == null)
        {
            throw new SmartMeterNotFoundException(policy.SmartMeterId);
        }

        var metadata = smartMeter.Metadata.OrderBy(m => m.ValidFrom).ToList();
        if (metadata.Count == 0)
        {
            // If no metadata given, policy location resolution can not match because there is no location reference for the measurements.
            return 0;
        }

        var specification = new MetadataLocationResolutionSpecification(policy.LocationResolution);
        var count = 0;
        for (var i = 0; i < metadata!.Count; i += 1)
        {
            if (!specification.IsSatisfiedBy(metadata[i]))
            {
                continue;
            }

            var nextIndex = i + 1;
            DateTime? startAt = metadata[i].ValidFrom;
            DateTime? endAt = nextIndex >= metadata!.Count ? null : metadata[i + 1].ValidFrom;
            count += await measurementRepository.GetMeasurementCountBySmartMeterAndResolution(policy.SmartMeterId,
                policy.MeasurementResolution, startAt, endAt, tenant);
        }

        return count;
    }

    private async Task<List<PolicyDto>> ToPolicyDtoListWithMeasurementCount(List<Policy> policies)
    {
        var policyDtoList = new List<PolicyDto>();
        foreach (var policy in policies)
        {
            var measurementCount =
                await measurementRepository.GetMeasurementCountBySmartMeterAndResolution(policy.SmartMeterId,
                    policy.MeasurementResolution, null, null);
            policyDtoList.Add(PolicyDto.FromPolicy(policy, measurementCount));
        }

        return policyDtoList;
    }
}