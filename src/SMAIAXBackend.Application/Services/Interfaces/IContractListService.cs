using SMAIAXBackend.Application.DTOs;
using SMAIAXBackend.Domain.Model.Enums;

namespace SMAIAXBackend.Application.Services.Interfaces;

public interface IContractListService
{
    Task<List<ContractOverviewDto>> GetContractsAsync();
    Task<ContractDto> GetContractByIdAsync(Guid contractId);

    Task<MeasurementListDto> GetMeasurementsByContractIdAsync(Guid contractId,
        MeasurementResolution? measurementResolution, DateTime? startAt, DateTime? endAt);
}