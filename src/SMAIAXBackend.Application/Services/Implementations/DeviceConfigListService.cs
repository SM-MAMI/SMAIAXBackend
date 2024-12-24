using SMAIAXBackend.Application.DTOs;
using SMAIAXBackend.Application.Exceptions;
using SMAIAXBackend.Application.Services.Interfaces;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;
using SMAIAXBackend.Domain.Repositories;

namespace SMAIAXBackend.Application.Services.Implementations;

public class DeviceConfigListService(
    IVaultRepository vaultRepository,
    ISmartMeterRepository smartMeterRepository,
    IEncryptionService encryptionService
) : IDeviceConfigListService
{
    public async Task<DeviceConfigDto> GetDeviceConfigByDeviceIdAsync(Guid id)
    {
        var (username, password, topic) = await vaultRepository.GetMqttBrokerCredentialsAsync(new SmartMeterId(id));

        if (username == null || password == null || topic == null)
        {
            throw new DeviceConfigNotFoundException(id);
        }

        var smartMeter = await smartMeterRepository.GetSmartMeterByIdAsync(new SmartMeterId(id));
        if (smartMeter == null)
        {
            throw new SmartMeterNotFoundException(id);
        }

        var deviceConfigDto = new DeviceConfigDto(
            encryptionService.Encrypt(username, smartMeter.PublicKey),
            encryptionService.Encrypt(password, smartMeter.PublicKey), 
            topic, 
            smartMeter.PublicKey);

        return deviceConfigDto;
    }
}