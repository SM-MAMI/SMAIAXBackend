using Moq;

using SMAIAXBackend.Application.DTOs;
using SMAIAXBackend.Application.Exceptions;
using SMAIAXBackend.Application.Services.Implementations;
using SMAIAXBackend.Application.Services.Interfaces;
using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.Enums;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;
using SMAIAXBackend.Domain.Repositories;

namespace SMAIAXBackend.Application.UnitTests;

[TestFixture]
public class MeasurementListServiceTests
{
    private Mock<IMeasurementRepository> _measurementRepositoryMock;
    private Mock<ISmartMeterListService> _smartMeterListServiceMock;
    private MeasurementListService _measurementListService;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _measurementRepositoryMock = new Mock<IMeasurementRepository>();
        _smartMeterListServiceMock = new Mock<ISmartMeterListService>();
        _measurementListService =
            new MeasurementListService(_measurementRepositoryMock.Object, _smartMeterListServiceMock.Object);
    }

    [Test]
    public async Task
        GivenSmartMeterIdAndStartAtAndEndAt_WhenGetMeasurementsBySmartMeterAsync_ThenReturnExpectedMeasurements()
    {
        // Given
        var smartMeterId = new SmartMeterId(Guid.NewGuid());
        var startAt = DateTime.UtcNow;
        var endAt = DateTime.UtcNow.AddHours(1);
        var smartMeterDto = new SmartMeterDto(smartMeterId.Id, "my smart meter", []);
        var measurementsExpected = new List<Measurement> { new() };

        _smartMeterListServiceMock.Setup(x => x.GetSmartMeterByIdAsync(smartMeterId.Id)).ReturnsAsync(smartMeterDto);
        _measurementRepositoryMock
            .Setup(x => x.GetMeasurementsBySmartMeterAsync(smartMeterId, It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .ReturnsAsync((measurementsExpected, 5));

        // When
        var (measurementsActual, countActual) =
            await _measurementListService.GetMeasurementsBySmartMeterAndResolutionAsync(smartMeterId.Id,
                MeasurementResolution.Raw, startAt, endAt);

        // Then
        Assert.That(measurementsActual, Is.Not.Null);
        Assert.That(measurementsActual, Has.Count.EqualTo(measurementsExpected.Count));
        Assert.That(countActual, Is.EqualTo(5));
    }

    [Test]
    public async Task
        GivenSmartMeterIdAndStartAtAndEndAt_WhenGetMeasurementsBySmartMeterAndResolutionAsync_ThenReturnExpectedMeasurements()
    {
        // Given
        var smartMeterId = new SmartMeterId(Guid.NewGuid());
        var startAt = DateTime.UtcNow;
        var endAt = DateTime.UtcNow.AddHours(1);
        var smartMeterDto = new SmartMeterDto(smartMeterId.Id, "my smart meter", []);
        var measurementsExpected = new List<AggregatedMeasurement>();

        _smartMeterListServiceMock.Setup(x => x.GetSmartMeterByIdAsync(smartMeterId.Id)).ReturnsAsync(smartMeterDto);
        _measurementRepositoryMock
            .Setup(x => x.GetAggregatedMeasurementsBySmartMeterAsync(smartMeterId, MeasurementResolution.Hour,
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>()))
            .ReturnsAsync((measurementsExpected, 5));

        // When
        var (measurementsActual, countActual) =
            await _measurementListService.GetMeasurementsBySmartMeterAndResolutionAsync(smartMeterId.Id,
                MeasurementResolution.Hour, new List<(DateTime?, DateTime?)>() { (startAt, endAt) });

        // Then
        Assert.That(measurementsActual, Is.Not.Null);
        Assert.That(measurementsActual, Has.Count.EqualTo(measurementsExpected.Count));
        Assert.That(countActual, Is.EqualTo(5));
    }

    [Test]
    public void
        GivenSmartMeterIdAndStartAtAndEndAt_WhenGetMeasurementsBySmartMeterAsync_ThenThrowSmartMeterNotFoundException()
    {
        // Given
        var smartMeterId = Guid.NewGuid();
        var startAt = DateTime.UtcNow;
        var endAt = DateTime.UtcNow.AddHours(1);

        _smartMeterListServiceMock.Setup(x => x.GetSmartMeterByIdAsync(smartMeterId))
            .ThrowsAsync(new SmartMeterNotFoundException(new SmartMeterId(smartMeterId)));

        // When ... Then
        Assert.ThrowsAsync<SmartMeterNotFoundException>(async () =>
            await _measurementListService.GetMeasurementsBySmartMeterAndResolutionAsync(smartMeterId,
                It.IsAny<MeasurementResolution>(), startAt, endAt)
        );
    }

    [Test]
    public void
        GivenSmartMeterIdAndStartAtAndEndAt_WhenGetMeasurementsBySmartMeterAndResolutionAsync_ThenThrowSmartMeterNotFoundException()
    {
        // Given
        var smartMeterId = Guid.NewGuid();

        _smartMeterListServiceMock.Setup(x => x.GetSmartMeterByIdAsync(smartMeterId))
            .ThrowsAsync(new SmartMeterNotFoundException(new SmartMeterId(smartMeterId)));

        // When ... Then
        Assert.ThrowsAsync<SmartMeterNotFoundException>(async () =>
            await _measurementListService.GetMeasurementsBySmartMeterAndResolutionAsync(smartMeterId,
                MeasurementResolution.Week, null)
        );
    }

    [Test]
    public void
        GivenSmartMeterIdAndStartAtAndEndAt_WhenGetMeasurementsBySmartMeterAsync_ThenThrowInvalidTimeRangeException()
    {
        // Given
        var smartMeterId = new SmartMeterId(Guid.NewGuid());
        var startAt = DateTime.UtcNow;
        var endAt = DateTime.UtcNow.AddHours(-1);
        var smartMeterDto = new SmartMeterDto(smartMeterId.Id, "Smart Meter 1", []);

        _smartMeterListServiceMock.Setup(x => x.GetSmartMeterByIdAsync(smartMeterId.Id)).ReturnsAsync(smartMeterDto);

        // When ... Then
        Assert.ThrowsAsync<InvalidTimeRangeException>(async () =>
            await _measurementListService.GetMeasurementsBySmartMeterAndResolutionAsync(smartMeterId.Id,
                It.IsAny<MeasurementResolution>(), startAt, endAt)
        );
    }

    [Test]
    public void
        GivenSmartMeterIdAndStartAtAndEndAt_WhenGetMeasurementsBySmartMeterAndResolutionAsync_ThenThrowInvalidTimeRangeException()
    {
        // Given
        var smartMeterId = new SmartMeterId(Guid.NewGuid());
        var startAt = DateTime.UtcNow;
        var endAt = DateTime.UtcNow.AddHours(-1);
        var smartMeterDto = new SmartMeterDto(smartMeterId.Id, "Smart Meter 1", []);

        _smartMeterListServiceMock.Setup(x => x.GetSmartMeterByIdAsync(smartMeterId.Id)).ReturnsAsync(smartMeterDto);

        // When ... Then
        Assert.ThrowsAsync<InvalidTimeRangeException>(async () =>
            await _measurementListService.GetMeasurementsBySmartMeterAndResolutionAsync(smartMeterId.Id,
                MeasurementResolution.Raw, new List<(DateTime?, DateTime?)>() { (startAt, endAt) })
        );
    }
}