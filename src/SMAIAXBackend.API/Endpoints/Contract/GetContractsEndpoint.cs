using Microsoft.AspNetCore.Http.HttpResults;

using SMAIAXBackend.Application.DTOs;
using SMAIAXBackend.Application.Services.Interfaces;

namespace SMAIAXBackend.API.Endpoints.Contract;

public static class GetContractsEndpoint
{
    public static async Task<Ok<List<ContractOverviewDto>>> Handle(IContractListService contractListService)
    {
        var contracts = await contractListService.GetContractsAsync();

        return TypedResults.Ok(contracts);
    }
}