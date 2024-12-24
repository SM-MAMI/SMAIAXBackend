using SMAIAXBackend.Application.Services.Interfaces;
using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.ValueObjects;
using SMAIAXBackend.Domain.Repositories;
using SMAIAXBackend.Domain.Repositories.Transactions;

namespace SMAIAXBackend.Application.Services.Implementations;

public class OrderService(
    ISmartMeterRepository smartMeterRepository,
    IMqttBrokerRepository mqttBrokerRepository,
    IVaultRepository vaultRepository,
    IKeyGenerationService keyGenerationService,
    ITransactionManager transactionManager) : IOrderService
{
    public async Task<ConnectorSerialNumber> OrderSmartMeterConnectorAsync()
    {
        var smartMeterId = smartMeterRepository.NextIdentity();
        var connectorSerialNumber = new ConnectorSerialNumber(Guid.NewGuid());

        //We would now trigger a process of burning the private key into the fuse of the connector for the smart meter
        //This however was out of scope for the project, therefore the private key is not used at this place
        var (publicKey, privateKey) = keyGenerationService.GenerateKeys();

        await transactionManager.ReadCommittedTransactionScope(async () =>
        {
            var smartMeter = SmartMeter.Create(smartMeterId, connectorSerialNumber, publicKey);
            await smartMeterRepository.AddAsync(smartMeter);

            string topic = $"smartmeter/{smartMeterId}";
            string username = $"smartmeter-{smartMeterId}";
            string password = $"{Guid.NewGuid()}-{Guid.NewGuid()}";
            await vaultRepository.SaveMqttBrokerCredentialsAsync(smartMeterId, topic, username, password);
            await mqttBrokerRepository.CreateMqttUserAsync(topic, username, password);
        });

        return connectorSerialNumber;
    }
}