using SMAIAXBackend.Application.DTOs;

namespace SMAIAXBackend.Application.Services.Interfaces;

public interface ISmartMeterCreateService
{
    Task<Guid> AssignSmartMeterAsync(SmartMeterAssignDto smartMeterAssignDto);
    Task<Guid> AddMetadataAsync(Guid smartMeterId, MetadataCreateDto metadataCreateDto);
}