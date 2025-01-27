using Microsoft.Extensions.Logging;

using SMAIAXBackend.Application.DTOs;
using SMAIAXBackend.Application.Exceptions;
using SMAIAXBackend.Application.Services.Interfaces;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;
using SMAIAXBackend.Domain.Repositories;

namespace SMAIAXBackend.Application.Services.Implementations;

public class ContractListService(
    IContractRepository contractRepository,
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

            var policyDto = await policyListService.GetPolicyByTenantAsync(vendorTenant, contract.PolicyId);

            var contractOverviewDto = ContractOverviewDto.FromContract(contract, policyDto);
            contractOverviewDtos.Add(contractOverviewDto);
        }

        return contractOverviewDtos;
    }

    public async Task<ContractDto> GetContractByIdAsync(Guid contractId)
    {
        var currentTenant = await tenantContextService.GetCurrentTenantAsync();
        var contract = await contractRepository.GetContractByIdAsync(currentTenant.Id, new ContractId(contractId));
        if (contract == null)
        {
            throw new ContractNotFoundException(contractId);
        }

        var vendorTenant = await tenantRepository.GetByIdAsync(contract.VendorId);
        if (vendorTenant == null)
        {
            logger.LogError("Tenant with id '{TenantId}' not found", contract.VendorId.Id);
            throw new TenantNotFoundException(contract.VendorId.Id);
        }

        var policyDto = await policyListService.GetPolicyByTenantAsync(vendorTenant, contract.PolicyId);
        var measurementListDto = await policyListService.GetMeasurementsByPolicyIdAsync(policyDto.Id, vendorTenant);

        return ContractDto.FromContract(contract, policyDto, measurementListDto);
    }
}