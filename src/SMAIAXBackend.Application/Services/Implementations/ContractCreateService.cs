using SMAIAXBackend.Application.DTOs;
using SMAIAXBackend.Application.Exceptions;
using SMAIAXBackend.Application.Services.Interfaces;
using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;
using SMAIAXBackend.Domain.Repositories;

namespace SMAIAXBackend.Application.Services.Implementations;

public class ContractCreateService(
    IContractRepository contractRepository,
    IPolicyRepository policyRepository,
    ITenantRepository tenantRepository,
    ITenantContextService tenantContextService
) : IContractCreateService
{
    public async Task<Guid> CreateContractAsync(ContractCreateDto contractCreateDto)
    {
        var contractId = contractRepository.NextIdentity();

        var currentTenant = await tenantContextService.GetCurrentTenantAsync();
        var tenants = await tenantRepository.GetAllAsync();
        var requiredPolicyId = new PolicyId(contractCreateDto.PolicyId);
        Policy? policy = null;
        TenantId? vendorId = null;
        foreach (var tenant in tenants.Where(t => !t.Equals(currentTenant)))
        {
            policy = await policyRepository.GetPolicyByTenantAsync(tenant, requiredPolicyId);
            if (policy == null)
            {
                continue;
            }

            vendorId = tenant.Id;
            break;
        }

        if (policy == null)
        {
            throw new PolicyNotFoundException(contractCreateDto.PolicyId);
        }

        var createdAt = DateTime.UtcNow;
        var contract = Contract.Create(contractId, createdAt, new PolicyId(contractCreateDto.PolicyId),
            currentTenant.Id, vendorId!);
        await contractRepository.AddAsync(contract);

        return contractId.Id;
    }
}