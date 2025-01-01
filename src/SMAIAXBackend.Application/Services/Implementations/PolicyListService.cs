using Microsoft.Extensions.Logging;

using SMAIAXBackend.Application.DTOs;
using SMAIAXBackend.Application.Exceptions;
using SMAIAXBackend.Application.Services.Interfaces;
using SMAIAXBackend.Domain.Handlers;
using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.Enums;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;
using SMAIAXBackend.Domain.Repositories;
using SMAIAXBackend.Domain.Specifications;

namespace SMAIAXBackend.Application.Services.Implementations;

public class PolicyListService(
    IMeasurementHandler measurementHandler,
    IPolicyRepository policyRepository,
    ITenantRepository tenantRepository,
    ITenantContextService tenantContextService,
    ILogger<PolicyListService> logger) : IPolicyListService
{
    public async Task<List<PolicyDto>> GetPoliciesBySmartMeterIdAsync(SmartMeterId smartMeterId)
    {
        var policies = await policyRepository.GetPoliciesBySmartMeterIdAsync(smartMeterId);
        return policies.Select(PolicyDto.FromPolicy).ToList();
    }

    public async Task<List<PolicyDto>> GetPoliciesAsync()
    {
        var policies = await policyRepository.GetPoliciesAsync();
        return policies.Select(PolicyDto.FromPolicy).ToList();
    }

    public async Task<List<PolicyDto>> GetFilteredPoliciesAsync(decimal? maxPrice,
        MeasurementResolution? measurementResolution)
    {
        var matchingPolicies = new List<PolicyDto>();
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

        var currentTenant = await tenantContextService.GetCurrentTenantAsync();
        var tenants = await tenantRepository.GetAllAsync();

        foreach (var tenant in tenants.Where(t => !t.Equals(currentTenant)))
        {
            var policies = await policyRepository.GetPoliciesByTenantAsync(tenant);
            var filteredPolicies = policies.Where(policy => specification.IsSatisfiedBy(policy)).ToList();
            matchingPolicies.AddRange(filteredPolicies.Select(PolicyDto.FromPolicy));
        }

        return matchingPolicies;
    }

    public async Task<List<MeasurementRawDto>> GetMeasurementsByPolicyIdAsync(Guid policyId)
    {
        var policy = await policyRepository.GetPolicyByIdAsync(new PolicyId(policyId));

        if (policy == null)
        {
            logger.LogError("Policy with id '{policyId}' not found.", policyId);
            throw new PolicyNotFoundException(policyId);
        }

        var measurements = await measurementHandler.GetMeasurementsByPolicyAsync(policy);
        return measurements.Select(MeasurementRawDto.FromMeasurement).ToList();
    }
}