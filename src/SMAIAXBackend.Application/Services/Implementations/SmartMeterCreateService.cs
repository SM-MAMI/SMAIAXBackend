using Microsoft.Extensions.Logging;

using SMAIAXBackend.Application.DTOs;
using SMAIAXBackend.Application.Exceptions;
using SMAIAXBackend.Application.Services.Interfaces;
using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.ValueObjects;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;
using SMAIAXBackend.Domain.Repositories;
using SMAIAXBackend.Domain.Repositories.Transactions;

namespace SMAIAXBackend.Application.Services.Implementations;

public class SmartMeterCreateService(
    ISmartMeterRepository smartMeterRepository,
    IMqttBrokerRepository mqttBrokerRepository,
    IVaultRepository vaultRepository,
    ITransactionManager transactionManager,
    ILogger<SmartMeterCreateService> logger) : ISmartMeterCreateService
{
    public async Task<Guid> AssignSmartMeterAsync(SmartMeterAssignDto smartMeterAssignDto)
    {
        var smartMeter =
            await smartMeterRepository.GetSmartMeterBySerialNumberAsync(
                new ConnectorSerialNumber(smartMeterAssignDto.SerialNumber));

        if (smartMeter == null)
        {
            logger.LogError("Smart meter with id '{SerialNumber} not found.", smartMeterAssignDto.SerialNumber);
            throw new SmartMeterNotFoundException(new ConnectorSerialNumber(smartMeterAssignDto.SerialNumber));
        }

        if (smartMeterAssignDto.Metadata != null)
        {
            var metadataId = smartMeterRepository.NextMetadataIdentity();
            var locationDto = smartMeterAssignDto.Metadata.Location;
            var location = locationDto != null
                ? new Location(locationDto.StreetName,
                    locationDto.City,
                    locationDto.State, locationDto.Country,
                    locationDto.Continent)
                : null;
            var metadata = Metadata.Create(metadataId, smartMeterAssignDto.Metadata.ValidFrom, location,
                smartMeterAssignDto.Metadata.HouseholdSize, smartMeter.Id);
            smartMeter.AddMetadata(metadata);
        }

        // TODO: Remove this check and add a validation attribute to the DTO
        if (String.IsNullOrEmpty(smartMeterAssignDto.Name))
        {
            logger.LogError("Smart meter name is required.");
            throw new SmartMeterNameRequiredException();
        }

        await transactionManager.ReadCommittedTransactionScope(async () =>
        {
            smartMeter.Update(smartMeterAssignDto.Name);
            await smartMeterRepository.UpdateAsync(smartMeter);

            string topic = $"smartmeter/{smartMeter.Id.Id}";
            string username = $"smartmeter-{smartMeter.Id.Id}";
            string password = $"{Guid.NewGuid()}-{Guid.NewGuid()}";
            await vaultRepository.SaveMqttBrokerCredentialsAsync(smartMeter.Id, topic, username, password);
            await mqttBrokerRepository.CreateMqttUserAsync(topic, username, password);
        });

        return smartMeter.Id.Id;
    }

    public async Task<Guid> AddMetadataAsync(Guid smartMeterId, MetadataCreateDto metadataCreateDto)
    {
        var smartMeter =
            await smartMeterRepository.GetSmartMeterByIdAsync(new SmartMeterId(smartMeterId));

        if (smartMeter == null)
        {
            logger.LogError("Smart meter with id '{SmartMeterId} not found.", smartMeterId);
            throw new SmartMeterNotFoundException(new SmartMeterId(smartMeterId));
        }

        var metadataId = smartMeterRepository.NextMetadataIdentity();
        var locationDto = metadataCreateDto.Location;
        var location = locationDto != null
            ? new Location(locationDto.StreetName, locationDto.City,
                locationDto.State, locationDto.Country, locationDto.Continent)
            : null;
        var metadata = Metadata.Create(metadataId, metadataCreateDto.ValidFrom, location,
            metadataCreateDto.HouseholdSize, smartMeter.Id);

        try
        {
            smartMeter.AddMetadata(metadata);
        }
        catch (ArgumentException ex)
        {
            logger.LogError(ex, "Metadata with id '{MetadataId}' already exists", metadataId.Id);
            throw new MetadataAlreadyExistsException(metadataId.Id);
        }

        await smartMeterRepository.UpdateAsync(smartMeter);

        return smartMeterId;
    }
}