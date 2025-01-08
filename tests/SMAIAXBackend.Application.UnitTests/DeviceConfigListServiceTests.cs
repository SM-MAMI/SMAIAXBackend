using Moq;

using SMAIAXBackend.Application.Exceptions;
using SMAIAXBackend.Application.Services.Implementations;
using SMAIAXBackend.Application.Services.Interfaces;
using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.ValueObjects;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;
using SMAIAXBackend.Domain.Repositories;

namespace SMAIAXBackend.Application.UnitTests;

[TestFixture]
public class DeviceConfigListServiceTests
{
    private Mock<IVaultRepository> _vaultRepositoryMock;
    private Mock<ISmartMeterRepository> _smartMeterRepositoryMock;
    private Mock<IEncryptionService> _encryptionServiceMock;
    private DeviceConfigListService _deviceConfigListService;

    [SetUp]
    public void Setup()
    {
        _vaultRepositoryMock = new Mock<IVaultRepository>();
        _smartMeterRepositoryMock = new Mock<ISmartMeterRepository>();
        _encryptionServiceMock = new Mock<IEncryptionService>();
        _deviceConfigListService = new DeviceConfigListService(
            _vaultRepositoryMock.Object,
            _smartMeterRepositoryMock.Object,
            _encryptionServiceMock.Object
        );
    }

    [Test]
    public async Task GivenValidDeviceId_WhenGetDeviceConfigByDeviceIdAsync_ThenDeviceConfigDtoIsReturned()
    {
        // Given
        var deviceId = Guid.NewGuid();
        var username = "testUser";
        var password = "testPassword";
        var topic = "test/topic";
        var publicKey = "publicKey123";
        var metadata = new List<Metadata>();

        var smartMeter = SmartMeter.Create(
            new SmartMeterId(deviceId),
            "Test Smart Meter",
            metadata,
            new ConnectorSerialNumber(Guid.NewGuid()),
            publicKey
        );

        _vaultRepositoryMock.Setup(repo => repo.GetMqttBrokerCredentialsAsync(It.IsAny<SmartMeterId>()))
            .ReturnsAsync((username, password, topic));

        _smartMeterRepositoryMock.Setup(repo => repo.GetSmartMeterByIdAsync(It.IsAny<SmartMeterId>()))
            .ReturnsAsync(smartMeter);

        _encryptionServiceMock.Setup(enc => enc.Encrypt(username, publicKey))
            .Returns("encryptedUsername");
        _encryptionServiceMock.Setup(enc => enc.Encrypt(password, publicKey))
            .Returns("encryptedPassword");

        // When
        var result = await _deviceConfigListService.GetDeviceConfigByDeviceIdAsync(deviceId);

        // Then
        Assert.That(result, Is.Not.Null);
        Assert.That(result.EncryptedMqttUsername, Is.EqualTo("encryptedUsername"));
        Assert.That(result.EncryptedMqttPassword, Is.EqualTo("encryptedPassword"));
        Assert.That(result.Topic, Is.EqualTo(topic));
        Assert.That(result.PublicKey, Is.EqualTo(publicKey));

        _vaultRepositoryMock.Verify(repo => repo.GetMqttBrokerCredentialsAsync(It.IsAny<SmartMeterId>()), Times.Once);
        _smartMeterRepositoryMock.Verify(repo => repo.GetSmartMeterByIdAsync(It.IsAny<SmartMeterId>()), Times.Once);
    }

    [Test]
    public void GivenInvalidDeviceId_WhenGetDeviceConfigByDeviceIdAsync_ThenDeviceConfigNotFoundExceptionIsThrown()
    {
        // Given
        var deviceId = Guid.NewGuid();

        _vaultRepositoryMock.Setup(repo => repo.GetMqttBrokerCredentialsAsync(It.IsAny<SmartMeterId>()))
            .ReturnsAsync((Username: null, Password: null, Topic: null));


        // When ... Then
        Assert.ThrowsAsync<DeviceConfigNotFoundException>(async () =>
            await _deviceConfigListService.GetDeviceConfigByDeviceIdAsync(deviceId));
    }

    [Test]
    public void GivenDeviceIdWithNonExistentSmartMeter_WhenGetDeviceConfigByDeviceIdAsync_ThenSmartMeterNotFoundExceptionIsThrown()
    {
        // Given
        var deviceId = Guid.NewGuid();
        var username = "testUser";
        var password = "testPassword";
        var topic = "test/topic";

        _vaultRepositoryMock.Setup(repo => repo.GetMqttBrokerCredentialsAsync(It.IsAny<SmartMeterId>()))
            .ReturnsAsync((username, password, topic));

        _smartMeterRepositoryMock.Setup(repo => repo.GetSmartMeterByIdAsync(It.IsAny<SmartMeterId>()))
            .ReturnsAsync((SmartMeter)null!);

        // When ... Then
        Assert.ThrowsAsync<SmartMeterNotFoundException>(async () =>
            await _deviceConfigListService.GetDeviceConfigByDeviceIdAsync(deviceId));
    }
}