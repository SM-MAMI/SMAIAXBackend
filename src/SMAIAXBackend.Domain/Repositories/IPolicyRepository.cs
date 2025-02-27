using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;

namespace SMAIAXBackend.Domain.Repositories;

public interface IPolicyRepository
{
    PolicyId NextIdentity();
    Task AddAsync(Policy policy);
    Task<List<Policy>> GetPoliciesBySmartMeterIdAsync(SmartMeterId smartMeterId);
    Task<List<Policy>> GetPoliciesByTenantAsync(Tenant tenant);
    Task<Policy?> GetPolicyByTenantAsync(Tenant tenant, PolicyId policyId);
    Task<List<Policy>> GetPoliciesAsync();
    Task<Policy?> GetPolicyByIdAsync(PolicyId policyId);
}