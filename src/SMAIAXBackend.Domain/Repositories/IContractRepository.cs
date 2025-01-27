using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;

namespace SMAIAXBackend.Domain.Repositories;

public interface IContractRepository
{
    ContractId NextIdentity();
    Task AddAsync(Contract contract);
    Task<List<Contract>> GetContractsForTenantAsync(TenantId buyerId);

    /// <summary>
    /// Reads the contract by buyer and contract id.
    /// </summary>
    /// <param name="buyerId">Id of the buyer for additional security.</param>
    /// <param name="contractId">Id of the contract.</param>
    /// <returns>The contract or null, if it does not exist or if buyer id does not equal the contract's buyer id.</returns>
    Task<Contract?> GetContractByIdAsync(TenantId buyerId, ContractId contractId);
}