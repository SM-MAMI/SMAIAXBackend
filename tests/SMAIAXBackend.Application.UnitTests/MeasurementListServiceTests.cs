using Moq;

using SMAIAXBackend.Application.Exceptions;
using SMAIAXBackend.Application.Services.Implementations;
using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.Enums;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;
using SMAIAXBackend.Domain.Repositories;

namespace SMAIAXBackend.Application.UnitTests;

[TestFixture]
public class MeasurementListServiceTests
{
    private Mock<IMeasurementRepository> _measurementRepositoryMock;
    private Mock<ISmartMeterRepository> _smartMeterRepositoryMock;
    private MeasurementListService _measurementListService;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _measurementRepositoryMock = new Mock<IMeasurementRepository>();
        _smartMeterRepositoryMock = new Mock<ISmartMeterRepository>();
        _measurementListService =
            new MeasurementListService(_measurementRepositoryMock.Object, _smartMeterRepositoryMock.Object);
    }

    [Test]
    public async Task
        GivenSmartMeterIdAndStartAtAndEndAt_WhenGetMeasurementsBySmartMeterAsync_ThenReturnExpectedMeasurements()
    {
        // Given
        var smartMeterId = new SmartMeterId(Guid.NewGuid());
        var startAt = DateTime.UtcNow;
        var endAt = DateTime.UtcNow.AddHours(1);
        var smartMeter = SmartMeter.Create(smartMeterId, "my smart meter", []);
        var measurementsExpected = new List<Measurement> { new() };

        _smartMeterRepositoryMock.Setup(x => x.GetSmartMeterByIdAsync(smartMeterId)).ReturnsAsync(smartMeter);
        _measurementRepositoryMock
            .Setup(x => x.GetMeasurementsBySmartMeterAsync(smartMeterId, It.IsAny<DateTime>(), It.IsAny<DateTime>(),
                null))
            .ReturnsAsync((measurementsExpected, 5));

        // When
        var measurementListActual =
            await _measurementListService.GetMeasurementsBySmartMeterAndResolutionAsync(smartMeterId.Id,
                MeasurementResolution.Raw, startAt, endAt);

        // Then
        Assert.That(measurementListActual, Is.Not.Null);
        Assert.That(measurementListActual.MeasurementAggregatedList, Is.Null);
        Assert.That(measurementListActual.MeasurementRawList, Has.Count.EqualTo(measurementsExpected.Count));
        Assert.That(measurementListActual.AmountOfMeasurements, Is.EqualTo(5));
    }

    [Test]
    public async Task
        GivenSmartMeterIdAndStartAtAndEndAt_WhenGetMeasurementsBySmartMeterAndResolutionAsync_ThenReturnExpectedMeasurements()
    {
        // Given
        var smartMeterId = new SmartMeterId(Guid.NewGuid());
        var startAt = DateTime.UtcNow;
        var endAt = DateTime.UtcNow.AddHours(1);
        var smartMeter = SmartMeter.Create(smartMeterId, "my smart meter", []);
        var measurementsExpected = new List<AggregatedMeasurement>();

        _smartMeterRepositoryMock.Setup(x => x.GetSmartMeterByIdAsync(smartMeterId)).ReturnsAsync(smartMeter);
        _measurementRepositoryMock
            .Setup(x => x.GetAggregatedMeasurementsBySmartMeterAsync(smartMeterId, MeasurementResolution.Hour,
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>(), null))
            .ReturnsAsync((measurementsExpected, 5));

        // When
        var measurementListActual = await _measurementListService.GetMeasurementsBySmartMeterAndResolutionAsync(
            smartMeterId.Id,
            MeasurementResolution.Hour, new List<(DateTime?, DateTime?)>() { (startAt, endAt) });

        // Then
        Assert.That(measurementListActual, Is.Not.Null);
        Assert.That(measurementListActual.MeasurementRawList, Is.Null);
        Assert.That(measurementListActual.MeasurementAggregatedList, Has.Count.EqualTo(measurementsExpected.Count));
        Assert.That(measurementListActual.AmountOfMeasurements, Is.EqualTo(5));
    }

    [Test]
    public void
        GivenSmartMeterIdAndStartAtAndEndAt_WhenGetMeasurementsBySmartMeterAsync_ThenThrowSmartMeterNotFoundException()
    {
        // Given
        var smartMeterId = Guid.NewGuid();
        var startAt = DateTime.UtcNow;
        var endAt = DateTime.UtcNow.AddHours(1);

        _smartMeterRepositoryMock.Setup(x => x.GetSmartMeterByIdAsync(It.Is<SmartMeterId>(sm => sm.Id == smartMeterId)))
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

        _smartMeterRepositoryMock.Setup(x => x.GetSmartMeterByIdAsync(It.Is<SmartMeterId>(sm => sm.Id == smartMeterId)))
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
        var smartMeter = SmartMeter.Create(smartMeterId, "Smart Meter 1", []);

        _smartMeterRepositoryMock.Setup(x => x.GetSmartMeterByIdAsync(smartMeterId)).ReturnsAsync(smartMeter);

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
        var smartMeter = SmartMeter.Create(smartMeterId, "Smart Meter 1", []);

        _smartMeterRepositoryMock.Setup(x => x.GetSmartMeterByIdAsync(smartMeterId)).ReturnsAsync(smartMeter);

        // When ... Then
        Assert.ThrowsAsync<InvalidTimeRangeException>(async () =>
            await _measurementListService.GetMeasurementsBySmartMeterAndResolutionAsync(smartMeterId.Id,
                MeasurementResolution.Raw, new List<(DateTime?, DateTime?)>() { (startAt, endAt) })
        );
    }
}