using Microsoft.Extensions.Logging;

using Moq;

using SMAIAXBackend.Application.Services.Implementations;
using SMAIAXBackend.Application.Services.Interfaces;
using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.ValueObjects;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;
using SMAIAXBackend.Domain.Repositories;
using SMAIAXBackend.Domain.Repositories.Transactions;

namespace SMAIAXBackend.Application.UnitTests;

[TestFixture]
public class OrderServiceUnitTests
{
    private Mock<ISmartMeterRepository> _smartMeterRepositoryMock;
    private Mock<IMqttBrokerRepository> _mqttBrokerRepositoryMock;
    private Mock<IVaultRepository> _vaultRepositoryMock;
    private Mock<IKeyGenerationService> _keyGenerationServiceMock;
    private Mock<ITransactionManager> _transactionManagerMock;
    private OrderService _orderService;

    [SetUp]
    public void Setup()
    {
        _smartMeterRepositoryMock = new Mock<ISmartMeterRepository>();
        _mqttBrokerRepositoryMock = new Mock<IMqttBrokerRepository>();
        _vaultRepositoryMock = new Mock<IVaultRepository>();
        _keyGenerationServiceMock = new Mock<IKeyGenerationService>();
        _transactionManagerMock = new Mock<ITransactionManager>();
        _orderService = new OrderService(_smartMeterRepositoryMock.Object,
            _mqttBrokerRepositoryMock.Object, _vaultRepositoryMock.Object, _keyGenerationServiceMock.Object,
            _transactionManagerMock.Object);
    }

    [Test]
    public async Task GivenOrderSmartMeterConnector_WhenOrderSmartMeterConnectorAsync_ThenConnectorSerialNumberIsReturned()
    {
        // Given
        var smartMeterId = new SmartMeterId(Guid.NewGuid());
        var publicKey = "SamplePublicKey";
        var privateKey = "SamplePrivateKey";

        _smartMeterRepositoryMock.Setup(repo => repo.NextIdentity()).Returns(smartMeterId);
        _keyGenerationServiceMock.Setup(service => service.GenerateKeys()).Returns((publicKey, privateKey));
        _transactionManagerMock.Setup(manager => manager.ReadCommittedTransactionScope(It.IsAny<Func<Task>>()))
            .Callback<Func<Task>>(async action => await action());

        ConnectorSerialNumber? connectorSerialNumberActual = null;
        _smartMeterRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<SmartMeter>()))
            .Callback<SmartMeter>(sm => connectorSerialNumberActual = sm.ConnectorSerialNumber);

        // When
        var returnedConnectorSerialNumber = await _orderService.OrderSmartMeterConnectorAsync();

        // Then
        Assert.That(connectorSerialNumberActual, Is.Not.Null);
        Assert.That(returnedConnectorSerialNumber, Is.EqualTo(connectorSerialNumberActual));

        _smartMeterRepositoryMock.Verify(repo => repo.NextIdentity(), Times.Once);
        _keyGenerationServiceMock.Verify(service => service.GenerateKeys(), Times.Once);

        _transactionManagerMock.Verify(manager => manager.ReadCommittedTransactionScope(It.IsAny<Func<Task>>()), Times.Once);

        _smartMeterRepositoryMock.Verify(repo => repo.AddAsync(It.Is<SmartMeter>(sm =>
            sm.Id == smartMeterId &&
            sm.ConnectorSerialNumber.Equals(connectorSerialNumberActual) &&
            sm.PublicKey == publicKey
        )), Times.Once);

        _vaultRepositoryMock.Verify(vault => vault.SaveMqttBrokerCredentialsAsync(
            smartMeterId, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);

        _mqttBrokerRepositoryMock.Verify(broker => broker.CreateMqttUserAsync(
            It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
}