using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using SMAIAXBackend.Application.DTOs;
using SMAIAXBackend.Application.Services.Interfaces;

namespace SMAIAXBackend.API.Endpoints.Contract;

public static class GetContractByIdEndpoint
{
    public static async Task<Ok<ContractDto>> Handle(IContractListService contractListService, [FromRoute] Guid id)
    {
        var contract = await contractListService.GetContractByIdAsync(id);

        return TypedResults.Ok(contract);
    }
}