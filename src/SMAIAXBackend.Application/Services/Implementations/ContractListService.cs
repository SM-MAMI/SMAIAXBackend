using Microsoft.Extensions.Logging;

using SMAIAXBackend.Application.DTOs;
using SMAIAXBackend.Application.Exceptions;
using SMAIAXBackend.Application.Services.Interfaces;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;
using SMAIAXBackend.Domain.Repositories;

namespace SMAIAXBackend.Application.Services.Implementations;

public class ContractListService(
    IContractRepository contractRepository,
    IPolicyRepository policyRepository,
    ITenantRepository tenantRepository,
    ITenantContextService tenantContextService,
    IPolicyListService policyListService,
    ILogger<ContractListService> logger) : IContractListService
{
    public async Task<List<ContractOverviewDto>> GetContractsAsync()
    {
        var currentTenant = await tenantContextService.GetCurrentTenantAsync();
        var contracts = await contractRepository.GetContractsForTenantAsync(currentTenant.Id);
        var contractOverviewDtos = new List<ContractOverviewDto>();

        foreach (var contract in contracts)
        {
            var vendorTenant = await tenantRepository.GetByIdAsync(new TenantId(contract.VendorId.Id));

            if (vendorTenant == null)
            {
                logger.LogError("Tenant with id '{TenantId}' not found", contract.VendorId.Id);
                throw new TenantNotFoundException(contract.VendorId.Id);
            }

            var policy = await policyRepository.GetPolicyByTenantAsync(vendorTenant, contract.PolicyId);

            if (policy == null)
            {
                logger.LogError("Policy with id '{PolicyId}' not found", contract.PolicyId.Id);
                throw new PolicyNotFoundException(contract.PolicyId.Id);
            }

            var measurementCount = await policyListService.GetMeasurementCountByTenantAndPolicyAsync(policy, vendorTenant);

            var contractOverviewDto = ContractOverviewDto.FromContract(contract, policy, measurementCount);
            contractOverviewDtos.Add(contractOverviewDto);
        }

        return contractOverviewDtos;
    }
}