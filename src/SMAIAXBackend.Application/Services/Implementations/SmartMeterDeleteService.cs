using Microsoft.Extensions.Logging;

using SMAIAXBackend.Application.Exceptions;
using SMAIAXBackend.Application.Services.Interfaces;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;
using SMAIAXBackend.Domain.Repositories;
using SMAIAXBackend.Domain.Repositories.Transactions;

namespace SMAIAXBackend.Application.Services.Implementations;

public class SmartMeterDeleteService(
    ISmartMeterRepository smartMeterRepository,
    IDeviceMappingRepository deviceMappingRepository,
    ITransactionManager transactionManager,
    ILogger<SmartMeterDeleteService> logger) : ISmartMeterDeleteService
{
    public async Task DeleteMetadataAsync(Guid smartMeterId, Guid metadataId)
    {
        var smartMeter =
            await smartMeterRepository.GetSmartMeterByIdAsync(new SmartMeterId(smartMeterId));

        if (smartMeter == null)
        {
            logger.LogError("Smart meter with id '{SmartMeterId} not found.", smartMeterId);
            throw new SmartMeterNotFoundException(new SmartMeterId(smartMeterId));
        }

        try
        {
            smartMeter.RemoveMetadata(new MetadataId(metadataId));
        }
        catch (ArgumentException ex)
        {
            logger.LogError(ex, "Metadata with id '{MetadataId}' not found", metadataId);
            throw new MetadataNotFoundException(metadataId);
        }

        await smartMeterRepository.UpdateAsync(smartMeter);
    }

    public async Task RemoveSmartMeterAsync(Guid smartMeterId)
    {
        var smartMeter = await smartMeterRepository.GetSmartMeterByIdAsync(new SmartMeterId(smartMeterId));
        if (smartMeter == null)
        {
            logger.LogError("Smart meter with id '{SmartMeterId} not found.", smartMeterId);
            throw new SmartMeterNotFoundException(new SmartMeterId(smartMeterId));
        }

        await smartMeterRepository.DeleteAsync(smartMeter);

        var deviceMapping =
            await deviceMappingRepository.GetDeviceMappingBySerialNumberAsync(smartMeter.ConnectorSerialNumber);
        if (deviceMapping == null)
        {
            throw new DeviceMappingNotFoundException(smartMeter.ConnectorSerialNumber.SerialNumber);
        }

        deviceMapping.DeleteAssignment();

        await deviceMappingRepository.UpdateAsync(deviceMapping);
    }
}