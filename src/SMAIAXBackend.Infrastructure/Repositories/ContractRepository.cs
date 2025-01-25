using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;
using SMAIAXBackend.Domain.Repositories;
using SMAIAXBackend.Infrastructure.DbContexts;

namespace SMAIAXBackend.Infrastructure.Repositories;

public class ContractRepository(ApplicationDbContext applicationDbContext) : IContractRepository
{
    public ContractId NextIdentity()
    {
        return new ContractId(Guid.NewGuid());
    }

    public async Task AddAsync(Contract contract)
    {
        await applicationDbContext.Contracts.AddAsync(contract);
        await applicationDbContext.SaveChangesAsync();
    }
}