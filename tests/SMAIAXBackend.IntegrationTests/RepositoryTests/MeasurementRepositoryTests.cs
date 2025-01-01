using System.Collections;

using Microsoft.EntityFrameworkCore;

using Npgsql;

using SMAIAXBackend.Domain.Model.Enums;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;

namespace SMAIAXBackend.IntegrationTests.RepositoryTests;

[TestFixture]
public class MeasurementRepositoryTests : TestBase
{
    [Test]
    public async Task GivenMeasurementsInRepository_WhenGetMeasurements_ThenMeasurementsAreReturned()
    {
        // Given
        var smartMeterId = new SmartMeterId(Guid.Parse("5e9db066-1b47-46cc-bbde-0b54c30167cd"));
        var startAt = DateTime.UtcNow.AddDays(-2);
        var endAt = DateTime.UtcNow;

        // When
        var measurementsActual =
            await _measurementRepository.GetMeasurementsBySmartMeterAsync(smartMeterId, startAt, endAt);

        // Then
        Assert.That(measurementsActual, Is.Not.Null);
        Assert.That(measurementsActual, Has.Count.EqualTo(1));
        Assert.That(measurementsActual[0].SmartMeterId, Is.EqualTo(smartMeterId));
    }

    [Test]
    public async Task GivenUnknownSmartMeterId_WhenGetMeasurements_ThenMeasurementsAreEmpty()
    {
        // Given
        var smartMeterId = new SmartMeterId(Guid.Parse("6f064ac0-f14f-448e-969e-0f5b2884b631"));
        var startAt = DateTime.UtcNow.AddDays(-1);
        var endAt = DateTime.UtcNow;

        // When
        var measurementsActual =
            await _measurementRepository.GetMeasurementsBySmartMeterAsync(smartMeterId, startAt, endAt);

        // Then
        Assert.That(measurementsActual, Is.Not.Null);
        Assert.That(measurementsActual, Is.Empty);
    }

    [Test]
    public async Task GivenSmartMeterInRepository_WhenGetMeasurements_ThenMeasurementsAreNotInRange()
    {
        // Given
        var smartMeterId = new SmartMeterId(Guid.Parse("5e9db066-1b47-46cc-bbde-0b54c30167cd"));
        var endAt = DateTime.UtcNow.AddDays(-1);
        var startAt = DateTime.UtcNow;

        // When
        var measurementsActual =
            await _measurementRepository.GetMeasurementsBySmartMeterAsync(smartMeterId, startAt, endAt);

        // Then
        Assert.That(measurementsActual, Is.Not.Null);
        Assert.That(measurementsActual, Is.Empty);
    }

    [Test]
    [TestCase(MeasurementResolution.Raw, 10,
        TestName = "GivenSmartMeterAndResolutionRaw_WhenGetMeasurements_ThenMeasurementsAreReturned")]
    [TestCase(MeasurementResolution.Minute, 9,
        TestName = "GivenSmartMeterAndResolutionMinute_WhenGetMeasurements_ThenMeasurementsAreReturned")]
    [TestCase(MeasurementResolution.QuarterHour, 9,
        TestName = "GivenSmartMeterAndResolutionQuarterHour_WhenGetMeasurements_ThenMeasurementsAreReturned")]
    [TestCase(MeasurementResolution.Hour, 5,
        TestName = "GivenSmartMeterAndResolutionHour_WhenGetMeasurements_ThenMeasurementsAreReturned")]
    [TestCase(MeasurementResolution.Day, 3,
        TestName = "GivenSmartMeterAndResolutionDay_WhenGetMeasurements_ThenMeasurementsAreReturned")]
    [TestCase(MeasurementResolution.Week, 1,
        TestName = "GivenSmartMeterAndResolutionWeek_WhenGetMeasurements_ThenMeasurementsAreReturned")]
    public async Task GivenSmartMeterAndResolution_WhenGetMeasurements_ThenMeasurementsAreReturned(
        MeasurementResolution measurementResolution, int expectedReturnCount)
    {
        // Given
        var smartMeterId = new SmartMeterId(Guid.Parse("5e9db066-1b47-46cc-bbde-0b54c30167cd"));
        // DateTime.UtcNow.AddDays(-1) is already in db.
        var date1 = new DateTime(2024, 1, 1, 1, 0, 0, DateTimeKind.Utc);
        var date2 = new DateTime(2024, 1, 1, 1, 15, 0, DateTimeKind.Utc);
        var date3 = new DateTime(2024, 1, 1, 1, 15, 1, DateTimeKind.Utc);
        var date4 = new DateTime(2024, 1, 1, 1, 30, 0, DateTimeKind.Utc);
        var date5 = new DateTime(2024, 1, 1, 1, 45, 0, DateTimeKind.Utc);
        var date6 = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc);
        var date7 = new DateTime(2024, 1, 2, 12, 0, 0, DateTimeKind.Utc);
        var date8 = new DateTime(2024, 1, 2, 13, 0, 0, DateTimeKind.Utc);
        var date9 = new DateTime(2024, 1, 3, 12, 0, 0, DateTimeKind.Utc);
        var date10 = new DateTime(2024, 1, 3, 12, 15, 0, DateTimeKind.Utc);
        await UpdateMeasurements(smartMeterId.Id, date1, date2, date3, date4, date5, date6, date7, date8, date9,
            date10);

        // When
        var measurementsActual =
            await _measurementRepository.GetMeasurementsBySmartMeterAndResolutionAsync(smartMeterId,
                measurementResolution, null, null);

        // Then
        Assert.That(measurementsActual, Is.Not.Null);
        Assert.That(measurementsActual, Has.Count.EqualTo(expectedReturnCount));
    }

    [Test]
    [TestCaseSource(nameof(CreateTestCaseData_GivenSmartMeterAndResolutionAndTimeSpan_WhenGetMeasurements_ThenMeasurementsAreReturned))]
    public async Task GivenSmartMeterAndResolutionAndTimeSpan_WhenGetMeasurements_ThenMeasurementsAreReturned(
        DateTime? start, DateTime? end, int expectedReturnCount)
    {
        // Given
        var smartMeterId = new SmartMeterId(Guid.Parse("5e9db066-1b47-46cc-bbde-0b54c30167cd"));
        // DateTime.UtcNow.AddDays(-1) is already in db.
        var date1 = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var date2 = new DateTime(2024, 1, 2, 0, 0, 0, DateTimeKind.Utc);
        var date3 = new DateTime(2024, 1, 3, 0, 0, 0, DateTimeKind.Utc);
        var date4 = new DateTime(2024, 1, 4, 0, 0, 0, DateTimeKind.Utc);
        var date5 = new DateTime(2024, 1, 5, 0, 0, 0, DateTimeKind.Utc);
        var date6 = new DateTime(2024, 1, 6, 0, 0, 0, DateTimeKind.Utc);
        var date7 = new DateTime(2024, 1, 7, 0, 0, 0, DateTimeKind.Utc);
        var date8 = new DateTime(2024, 1, 8, 0, 0, 0, DateTimeKind.Utc);
        var date9 = new DateTime(2024, 1, 9, 0, 0, 0, DateTimeKind.Utc);
        var date10 = new DateTime(2024, 1, 10, 0, 0, 0, DateTimeKind.Utc);
        await UpdateMeasurements(smartMeterId.Id, date1, date2, date3, date4, date5, date6, date7, date8, date9,
            date10);

        // When
        var measurementsActual =
            await _measurementRepository.GetMeasurementsBySmartMeterAndResolutionAsync(smartMeterId,
                MeasurementResolution.Raw, start, end);

        // Then
        Assert.That(measurementsActual, Is.Not.Null);
        Assert.That(measurementsActual, Has.Count.EqualTo(expectedReturnCount));
    }

    private async Task UpdateMeasurements(Guid smartMeterId, params DateTime[] timestamps)
    {
        await _tenant1DbContext.Database.OpenConnectionAsync();
        var deleteCommand = _tenant1DbContext.Database.GetDbConnection().CreateCommand();
        deleteCommand.CommandText = "DELETE FROM domain.\"Measurement\";";
        await deleteCommand.ExecuteNonQueryAsync();
        foreach (var ts in timestamps)
        {
            var sql = @"
                         INSERT INTO domain.""Measurement""(""positiveActivePower"", ""positiveActiveEnergyTotal"", ""negativeActivePower"", ""negativeActiveEnergyTotal"", ""reactiveEnergyQuadrant1Total"", ""reactiveEnergyQuadrant3Total"", ""totalPower"", ""currentPhase1"", ""voltagePhase1"", ""currentPhase2"", ""voltagePhase2"", ""currentPhase3"", ""voltagePhase3"", ""uptime"", ""timestamp"", ""smartMeterId"") 
                         VALUES (@positiveActivePower, @positiveActiveEnergyTotal, @negativeActivePower, @negativeActiveEnergyTotal, @reactiveEnergyQuadrant1Total, @reactiveEnergyQuadrant3Total, @totalPower, @currentPhase1, @voltagePhase1, @currentPhase2, @voltagePhase2, @currentPhase3, @voltagePhase3, @uptime, @timestamp, @smartMeterId);
                     ";
            await using var insertCommand = _tenant1DbContext.Database.GetDbConnection().CreateCommand();
            insertCommand.CommandText = sql;

            insertCommand.Parameters.Add(new NpgsqlParameter("@positiveActivePower", 160));
            insertCommand.Parameters.Add(new NpgsqlParameter("@positiveActiveEnergyTotal", 1137778));
            insertCommand.Parameters.Add(new NpgsqlParameter("@negativeActivePower", 1));
            insertCommand.Parameters.Add(new NpgsqlParameter("@negativeActiveEnergyTotal", 1));
            insertCommand.Parameters.Add(new NpgsqlParameter("@reactiveEnergyQuadrant1Total", 3837));
            insertCommand.Parameters.Add(new NpgsqlParameter("@reactiveEnergyQuadrant3Total", 717727));
            insertCommand.Parameters.Add(new NpgsqlParameter("@totalPower", 160));
            insertCommand.Parameters.Add(new NpgsqlParameter("@currentPhase1", 1.03));
            insertCommand.Parameters.Add(new NpgsqlParameter("@voltagePhase1", 229.80));
            insertCommand.Parameters.Add(new NpgsqlParameter("@currentPhase2", 0.42));
            insertCommand.Parameters.Add(new NpgsqlParameter("@voltagePhase2", 229.00));
            insertCommand.Parameters.Add(new NpgsqlParameter("@currentPhase3", 0.17));
            insertCommand.Parameters.Add(new NpgsqlParameter("@voltagePhase3", 229.60));
            insertCommand.Parameters.Add(new NpgsqlParameter("@uptime", "0000:01:49:41"));
            insertCommand.Parameters.Add(new NpgsqlParameter("@timestamp", ts));
            insertCommand.Parameters.Add(new NpgsqlParameter("@smartMeterId", smartMeterId));

            await insertCommand.ExecuteNonQueryAsync();
        }

        await _tenant1DbContext.Database.CloseConnectionAsync();
    }

    private static IEnumerable
        CreateTestCaseData_GivenSmartMeterAndResolutionAndTimeSpan_WhenGetMeasurements_ThenMeasurementsAreReturned()
    {
        yield return new TestCaseData(new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            new DateTime(2024, 1, 2, 0, 0, 0, DateTimeKind.Utc), 2)
        {
            TestName = "GivenSmartMeterAndResolutionAndTimeSpan_WhenGetMeasurements_ThenMeasurementsAreReturned"
        };

        yield return new TestCaseData(new DateTime(2024, 1, 2, 0, 0, 0, DateTimeKind.Utc), null, 9)
        {
            TestName = "GivenSmartMeterAndResolutionAndTimeSpanWithoutEnd_WhenGetMeasurements_ThenMeasurementsAreReturned"
        };

        yield return new TestCaseData(null, new DateTime(2024, 1, 2, 0, 0, 0, DateTimeKind.Utc), 2)
        {
            TestName = "GivenSmartMeterAndResolutionAndTimeSpanWithoutStart_WhenGetMeasurements_ThenMeasurementsAreReturned"
        };
    }
}