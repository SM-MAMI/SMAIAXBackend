using System.Collections;

using Microsoft.EntityFrameworkCore;

using Npgsql;

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
        var (measurementsActual, countActual) =
            await _measurementRepository.GetMeasurementsBySmartMeterAsync(smartMeterId, startAt, endAt);

        // Then
        Assert.That(measurementsActual, Is.Not.Null);
        Assert.That(measurementsActual, Has.Count.EqualTo(1));
        Assert.That(countActual, Is.EqualTo(1));
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
        var (measurementsActual, countActual) =
            await _measurementRepository.GetMeasurementsBySmartMeterAsync(smartMeterId, startAt, endAt);

        // Then
        Assert.That(measurementsActual, Is.Not.Null);
        Assert.That(measurementsActual, Is.Empty);
        Assert.That(countActual, Is.EqualTo(0));
    }

    [Test]
    public async Task GivenSmartMeterInRepository_WhenGetMeasurements_ThenMeasurementsAreNotInRange()
    {
        // Given
        var smartMeterId = new SmartMeterId(Guid.Parse("5e9db066-1b47-46cc-bbde-0b54c30167cd"));
        var endAt = DateTime.UtcNow.AddDays(-1);
        var startAt = DateTime.UtcNow;

        // When
        var (measurementsActual, countActual) =
            await _measurementRepository.GetMeasurementsBySmartMeterAsync(smartMeterId, startAt, endAt);

        // Then
        Assert.That(measurementsActual, Is.Not.Null);
        Assert.That(measurementsActual, Is.Empty);
        Assert.That(countActual, Is.EqualTo(0));
    }

    [Test]
    public async Task GivenSmartMeterInRepository_WhenGetMeasurementsWithoutDates_ThenMeasurementsAreReturned()
    {
        // Given
        var smartMeterId = new SmartMeterId(Guid.Parse("5e9db066-1b47-46cc-bbde-0b54c30167cd"));

        // When
        var (measurementsActual, countActual) =
            await _measurementRepository.GetMeasurementsBySmartMeterAsync(smartMeterId, null, null);

        // Then
        Assert.That(measurementsActual, Is.Not.Null);
        Assert.That(measurementsActual, Has.Count.EqualTo(1));
        Assert.That(countActual, Is.EqualTo(1));
    }

    [Test]
    [TestCaseSource(nameof(CreateTestCases_GivenSmartMeter_WhenGetMeasurementsPerMinute_ThenMeasurementsAreReturned))]
    public async Task GivenSmartMeter_WhenGetMeasurementsPerMinute_ThenMeasurementsAreReturned(DateTime? start,
        DateTime? end, int expectedMeasurementsCount)
    {
        // Given
        var smartMeterId = new SmartMeterId(Guid.Parse("5e9db066-1b47-46cc-bbde-0b54c30167cd"));
        // same minute
        var date1 = new DateTime(2024, 1, 1, 1, 1, 0, DateTimeKind.Utc);
        var date2 = new DateTime(2024, 1, 1, 1, 1, 1, DateTimeKind.Utc);
        var date3 = new DateTime(2024, 1, 1, 1, 1, 2, DateTimeKind.Utc);
        var date4 = new DateTime(2024, 1, 1, 1, 1, 3, DateTimeKind.Utc);
        var date5 = new DateTime(2024, 1, 1, 1, 1, 4, DateTimeKind.Utc);
        // same minute
        var date6 = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc);
        // same minute
        var date7 = new DateTime(2024, 1, 2, 12, 0, 0, DateTimeKind.Utc);
        var date8 = new DateTime(2024, 1, 2, 12, 0, 30, DateTimeKind.Utc);
        // different minutes
        var date9 = new DateTime(2024, 1, 3, 12, 0, 0, DateTimeKind.Utc);
        var date10 = new DateTime(2024, 2, 1, 12, 15, 0, DateTimeKind.Utc);
        var date11 = new DateTime(2024, 2, 2, 12, 15, 0, DateTimeKind.Utc);
        await UpdateMeasurements(smartMeterId.Id, date1, date2, date3, date4, date5, date6, date7, date8, date9,
            date10, date11);

        // When
        var (measurementsActual, countActual) =
            await _measurementRepository.GetAggregatedMeasurementsBySmartMeterAsync(smartMeterId,
                MeasurementResolution.Minute, start, end);

        // Then
        Assert.That(measurementsActual, Is.Not.Null);
        Assert.That(measurementsActual, Has.Count.EqualTo(expectedMeasurementsCount));
        Assert.That(countActual, Is.EqualTo(expectedMeasurementsCount));
    }

    [Test]
    [TestCaseSource(
        nameof(CreateTestCases_GivenSmartMeter_WhenGetMeasurementsPerQuarterHour_ThenMeasurementsAreReturned))]
    public async Task GivenSmartMeter_WhenGetMeasurementsPerQuarterHour_ThenMeasurementsAreReturned(DateTime? start,
        DateTime? end, int expectedMeasurementsCount)
    {
        // Given
        var smartMeterId = new SmartMeterId(Guid.Parse("5e9db066-1b47-46cc-bbde-0b54c30167cd"));
        // same quarter-hour
        var date1 = new DateTime(2024, 1, 1, 1, 0, 0, DateTimeKind.Utc);
        var date2 = new DateTime(2024, 1, 1, 1, 0, 1, DateTimeKind.Utc);
        // same quarter-hour
        var date3 = new DateTime(2024, 1, 1, 1, 15, 2, DateTimeKind.Utc);
        var date4 = new DateTime(2024, 1, 1, 1, 15, 3, DateTimeKind.Utc);
        var date5 = new DateTime(2024, 1, 1, 1, 15, 44, DateTimeKind.Utc);
        // same quarter-hour
        var date6 = new DateTime(2024, 1, 1, 2, 30, 0, DateTimeKind.Utc);
        // same quarter-hour
        var date7 = new DateTime(2024, 1, 2, 1, 45, 0, DateTimeKind.Utc);
        var date8 = new DateTime(2024, 1, 2, 1, 45, 30, DateTimeKind.Utc);
        // different quarter-hours
        var date9 = new DateTime(2024, 1, 3, 12, 0, 0, DateTimeKind.Utc);
        var date10 = new DateTime(2024, 2, 1, 12, 0, 0, DateTimeKind.Utc);
        await UpdateMeasurements(smartMeterId.Id, date1, date2, date3, date4, date5, date6, date7, date8, date9,
            date10);

        // When
        var (measurementsActual, countActual) =
            await _measurementRepository.GetAggregatedMeasurementsBySmartMeterAsync(smartMeterId,
                MeasurementResolution.QuarterHour, start, end);

        // Then
        Assert.That(measurementsActual, Is.Not.Null);
        Assert.That(measurementsActual, Has.Count.EqualTo(expectedMeasurementsCount));
        Assert.That(countActual, Is.EqualTo(expectedMeasurementsCount));
    }

    [Test]
    [TestCaseSource(nameof(CreateTestCases_GivenSmartMeter_WhenGetMeasurementsPerHour_ThenMeasurementsAreReturned))]
    public async Task GivenSmartMeter_WhenGetMeasurementsPerHour_ThenMeasurementsAreReturned(DateTime? start,
        DateTime? end, int expectedMeasurementsCount)
    {
        // Given
        var smartMeterId = new SmartMeterId(Guid.Parse("5e9db066-1b47-46cc-bbde-0b54c30167cd"));
        // same hour
        var date1 = new DateTime(2024, 1, 1, 1, 0, 0, DateTimeKind.Utc);
        var date2 = new DateTime(2024, 1, 1, 1, 15, 0, DateTimeKind.Utc);
        // same hour
        var date3 = new DateTime(2024, 1, 1, 6, 15, 2, DateTimeKind.Utc);
        var date4 = new DateTime(2024, 1, 1, 6, 15, 3, DateTimeKind.Utc);
        var date5 = new DateTime(2024, 1, 1, 6, 30, 45, DateTimeKind.Utc);
        // same hour
        var date6 = new DateTime(2024, 1, 2, 2, 30, 0, DateTimeKind.Utc);
        // same hour
        var date7 = new DateTime(2024, 1, 2, 23, 0, 0, DateTimeKind.Utc);
        var date8 = new DateTime(2024, 1, 2, 23, 45, 30, DateTimeKind.Utc);
        // different hours
        var date9 = new DateTime(2024, 1, 3, 12, 0, 0, DateTimeKind.Utc);
        var date10 = new DateTime(2024, 2, 1, 12, 0, 0, DateTimeKind.Utc);
        await UpdateMeasurements(smartMeterId.Id, date1, date2, date3, date4, date5, date6, date7, date8, date9,
            date10);

        // When
        var (measurementsActual, countActual) =
            await _measurementRepository.GetAggregatedMeasurementsBySmartMeterAsync(smartMeterId,
                MeasurementResolution.Hour, start, end);

        // Then
        Assert.That(measurementsActual, Is.Not.Null);
        Assert.That(measurementsActual, Has.Count.EqualTo(expectedMeasurementsCount));
        Assert.That(countActual, Is.EqualTo(expectedMeasurementsCount));
    }

    [Test]
    [TestCaseSource(nameof(CreateTestCases_GivenSmartMeter_WhenGetMeasurementsPerDay_ThenMeasurementsAreReturned))]
    public async Task GivenSmartMeter_WhenGetMeasurementsPerDay_ThenMeasurementsAreReturned(DateTime? start,
        DateTime? end, int expectedMeasurementsCount)
    {
        // Given
        var smartMeterId = new SmartMeterId(Guid.Parse("5e9db066-1b47-46cc-bbde-0b54c30167cd"));
        // same day
        var date1 = new DateTime(2024, 1, 1, 1, 0, 0, DateTimeKind.Utc);
        var date2 = new DateTime(2024, 1, 1, 1, 15, 0, DateTimeKind.Utc);
        // same day
        var date3 = new DateTime(2024, 1, 2, 6, 15, 2, DateTimeKind.Utc);
        var date4 = new DateTime(2024, 1, 2, 12, 15, 3, DateTimeKind.Utc);
        var date5 = new DateTime(2024, 1, 2, 23, 30, 45, DateTimeKind.Utc);
        // same day
        var date6 = new DateTime(2024, 1, 3, 2, 30, 0, DateTimeKind.Utc);
        // same day
        var date7 = new DateTime(2024, 1, 4, 23, 0, 0, DateTimeKind.Utc);
        var date8 = new DateTime(2024, 1, 4, 23, 45, 30, DateTimeKind.Utc);
        // different days
        var date9 = new DateTime(2024, 1, 5, 12, 0, 0, DateTimeKind.Utc);
        var date10 = new DateTime(2024, 2, 6, 12, 0, 0, DateTimeKind.Utc);
        await UpdateMeasurements(smartMeterId.Id, date1, date2, date3, date4, date5, date6, date7, date8, date9,
            date10);

        // When
        var (measurementsActual, countActual) =
            await _measurementRepository.GetAggregatedMeasurementsBySmartMeterAsync(smartMeterId,
                MeasurementResolution.Day, start, end);

        // Then
        Assert.That(measurementsActual, Is.Not.Null);
        Assert.That(measurementsActual, Has.Count.EqualTo(expectedMeasurementsCount));
        Assert.That(countActual, Is.EqualTo(expectedMeasurementsCount));
    }

    [Test]
    [TestCaseSource(nameof(CreateTestCases_GivenSmartMeter_WhenGetMeasurementsPerWeek_ThenMeasurementsAreReturned))]
    public async Task GivenSmartMeter_WhenGetMeasurementsPerWeek_ThenMeasurementsAreReturned(DateTime? start,
        DateTime? end, int expectedMeasurementsCount)
    {
        // Given
        var smartMeterId = new SmartMeterId(Guid.Parse("5e9db066-1b47-46cc-bbde-0b54c30167cd"));
        // same week
        var date1 = new DateTime(2024, 1, 1, 1, 0, 0, DateTimeKind.Utc);
        var date2 = new DateTime(2024, 1, 6, 1, 15, 0, DateTimeKind.Utc);
        // same week
        var date3 = new DateTime(2024, 1, 10, 6, 15, 2, DateTimeKind.Utc);
        var date4 = new DateTime(2024, 1, 10, 12, 15, 3, DateTimeKind.Utc);
        var date5 = new DateTime(2024, 1, 14, 23, 30, 45, DateTimeKind.Utc);
        // same week
        var date6 = new DateTime(2024, 1, 15, 2, 30, 0, DateTimeKind.Utc);
        // same week
        var date7 = new DateTime(2024, 1, 23, 23, 0, 0, DateTimeKind.Utc);
        var date8 = new DateTime(2024, 1, 24, 23, 45, 30, DateTimeKind.Utc);
        // same week
        var date9 = new DateTime(2024, 1, 31, 12, 0, 0, DateTimeKind.Utc);
        var date10 = new DateTime(2024, 2, 3, 12, 0, 0, DateTimeKind.Utc);
        await UpdateMeasurements(smartMeterId.Id, date1, date2, date3, date4, date5, date6, date7, date8, date9,
            date10);

        // When
        var (measurementsActual, countActual) =
            await _measurementRepository.GetAggregatedMeasurementsBySmartMeterAsync(smartMeterId,
                MeasurementResolution.Week, start, end);

        // Then
        Assert.That(measurementsActual, Is.Not.Null);
        Assert.That(measurementsActual, Has.Count.EqualTo(expectedMeasurementsCount));
        Assert.That(countActual, Is.EqualTo(expectedMeasurementsCount));
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

        // update views
        await _tenant1DbContext.Database.ExecuteSqlRawAsync(
            "CALL refresh_continuous_aggregate('\"domain\".\"MeasurementPerMinute\"', null, null);");
        await _tenant1DbContext.Database.ExecuteSqlRawAsync(
            "CALL refresh_continuous_aggregate('\"domain\".\"MeasurementPerQuarterHour\"', null, null);");
        await _tenant1DbContext.Database.ExecuteSqlRawAsync(
            "CALL refresh_continuous_aggregate('\"domain\".\"MeasurementPerHour\"', null, null);");
        await _tenant1DbContext.Database.ExecuteSqlRawAsync(
            "CALL refresh_continuous_aggregate('\"domain\".\"MeasurementPerDay\"', null, null);");
        await _tenant1DbContext.Database.ExecuteSqlRawAsync(
            "CALL refresh_continuous_aggregate('\"domain\".\"MeasurementPerWeek\"', null, null);");

        await _tenant1DbContext.Database.CloseConnectionAsync();
    }

    private static IEnumerable
        CreateTestCases_GivenSmartMeter_WhenGetMeasurementsPerMinute_ThenMeasurementsAreReturned()
    {
        yield return new TestCaseData(null, null, 6)
        {
            TestName = "GivenSmartMeter_WhenGetMeasurementsPerMinuteWithoutDateTimes_ThenMeasurementsAreReturned"
        };

        yield return new TestCaseData(new DateTime(2024, 1, 1, 1, 2, 0, DateTimeKind.Utc), null, 5)
        {
            TestName = "GivenSmartMeter_WhenGetMeasurementsPerMinuteWithStart_ThenMeasurementsAreReturned"
        };

        yield return new TestCaseData(null, new DateTime(2024, 1, 1, 1, 1, 0, DateTimeKind.Utc), 1)
        {
            TestName = "GivenSmartMeter_WhenGetMeasurementsPerMinuteWithEnd_ThenMeasurementsAreReturned"
        };

        yield return new TestCaseData(new DateTime(2024, 1, 1, 2, 30, 0, DateTimeKind.Utc),
            new DateTime(2024, 3, 1, 0, 0, 0, DateTimeKind.Utc), 5)
        {
            TestName = "GivenSmartMeter_WhenGetMeasurementsPerMinuteWithStartAndEnd_ThenMeasurementsAreReturned"
        };
    }

    private static IEnumerable
        CreateTestCases_GivenSmartMeter_WhenGetMeasurementsPerQuarterHour_ThenMeasurementsAreReturned()
    {
        yield return new TestCaseData(null, null, 6)
        {
            TestName =
                "GivenSmartMeter_WhenGetMeasurementsPerQuarterHourWithoutDateTimes_ThenMeasurementsAreReturned"
        };

        yield return new TestCaseData(new DateTime(2024, 1, 1, 1, 17, 15, DateTimeKind.Utc), null, 4)
        {
            TestName = "GivenSmartMeter_WhenGetMeasurementsPerQuarterHourWithStart_ThenMeasurementsAreReturned"
        };

        yield return new TestCaseData(null, new DateTime(2024, 1, 1, 1, 16, 0, DateTimeKind.Utc), 2)
        {
            TestName = "GivenSmartMeter_WhenGetMeasurementsPerQuarterHourWithEnd_ThenMeasurementsAreReturned"
        };

        yield return new TestCaseData(new DateTime(2024, 1, 1, 1, 1, 0, DateTimeKind.Utc),
            new DateTime(2024, 1, 2, 12, 1, 0, DateTimeKind.Utc), 3)
        {
            TestName = "GivenSmartMeter_WhenGetMeasurementsPerQuarterHourWithStartAndEnd_ThenMeasurementsAreReturned"
        };
    }

    private static IEnumerable
        CreateTestCases_GivenSmartMeter_WhenGetMeasurementsPerHour_ThenMeasurementsAreReturned()
    {
        yield return new TestCaseData(null, null, 6)
        {
            TestName = "GivenSmartMeter_WhenGetMeasurementsPerHourWithoutDateTimes_ThenMeasurementsAreReturned"
        };

        yield return new TestCaseData(new DateTime(2024, 1, 2, 2, 0, 0, DateTimeKind.Utc), null, 4)
        {
            TestName = "GivenSmartMeter_WhenGetMeasurementsPerHourWithStart_ThenMeasurementsAreReturned"
        };

        yield return new TestCaseData(null, new DateTime(2024, 1, 1, 1, 0, 0, DateTimeKind.Utc), 1)
        {
            TestName = "GivenSmartMeter_WhenGetMeasurementsPerHourWithEnd_ThenMeasurementsAreReturned"
        };

        yield return new TestCaseData(new DateTime(2024, 1, 1, 1, 0, 0, DateTimeKind.Utc),
            new DateTime(2024, 1, 2, 2, 30, 0, DateTimeKind.Utc), 3)
        {
            TestName = "GivenSmartMeter_WhenGetMeasurementsPerHourWithStartAndEnd_ThenMeasurementsAreReturned"
        };
    }

    private static IEnumerable
        CreateTestCases_GivenSmartMeter_WhenGetMeasurementsPerDay_ThenMeasurementsAreReturned()
    {
        yield return new TestCaseData(null, null, 6)
        {
            TestName = "GivenSmartMeter_WhenGetMeasurementsPerDayWithoutDateTimes_ThenMeasurementsAreReturned"
        };

        yield return new TestCaseData(new DateTime(2024, 1, 2, 23, 30, 0, DateTimeKind.Utc), null, 4)
        {
            TestName = "GivenSmartMeter_WhenGetMeasurementsPerDayWithStart_ThenMeasurementsAreReturned"
        };

        yield return new TestCaseData(null, new DateTime(2024, 1, 2, 23, 0, 0, DateTimeKind.Utc), 2)
        {
            TestName = "GivenSmartMeter_WhenGetMeasurementsPerDayWithEnd_ThenMeasurementsAreReturned"
        };

        yield return new TestCaseData(new DateTime(2024, 1, 3, 0, 0, 0, DateTimeKind.Utc),
            new DateTime(2024, 1, 5, 0, 0, 0, DateTimeKind.Utc), 3)
        {
            TestName = "GivenSmartMeter_WhenGetMeasurementsPerDayWithStartAndEnd_ThenMeasurementsAreReturned"
        };
    }

    private static IEnumerable
        CreateTestCases_GivenSmartMeter_WhenGetMeasurementsPerWeek_ThenMeasurementsAreReturned()
    {
        yield return new TestCaseData(null, null, 5)
        {
            TestName = "GivenSmartMeter_WhenGetMeasurementsPerWeekWithoutDateTimes_ThenMeasurementsAreReturned"
        };

        yield return new TestCaseData(new DateTime(2024, 1, 14, 5, 30, 0, DateTimeKind.Utc), null, 3)
        {
            TestName = "GivenSmartMeter_WhenGetMeasurementsPerWeekWithStart_ThenMeasurementsAreReturned"
        };

        yield return new TestCaseData(null, new DateTime(2024, 1, 14, 5, 30, 0, DateTimeKind.Utc), 2)
        {
            TestName = "GivenSmartMeter_WhenGetMeasurementsPerWeekWithEnd_ThenMeasurementsAreReturned"
        };

        yield return new TestCaseData(new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            new DateTime(2024, 1, 16, 0, 0, 0, DateTimeKind.Utc), 3)
        {
            TestName = "GivenSmartMeter_WhenGetMeasurementsPerWeekWithStartAndEnd_ThenMeasurementsAreReturned"
        };
    }
}