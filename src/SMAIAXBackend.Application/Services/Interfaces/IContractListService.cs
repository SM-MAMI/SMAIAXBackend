using SMAIAXBackend.Application.DTOs;

namespace SMAIAXBackend.Application.Services.Interfaces;

public interface IContractListService
{
    Task<List<ContractOverviewDto>> GetContractsAsync();
    Task<ContractDto> GetContractByIdAsync(Guid contractId);
}