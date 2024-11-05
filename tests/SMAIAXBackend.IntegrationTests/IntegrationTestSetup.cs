using DotNet.Testcontainers.Builders;

using Microsoft.Extensions.DependencyInjection;

using SMAIAXBackend.Domain.Repositories;
using SMAIAXBackend.Infrastructure.DbContexts;

using Testcontainers.PostgreSql;

namespace SMAIAXBackend.IntegrationTests;

[SetUpFixture]
internal static class IntegrationTestSetup
{
    private static PostgreSqlContainer _postgresContainer = null!;
    private static WebAppFactory _webAppFactory = null!;
    public static ApplicationDbContext ApplicationDbContext { get; private set; } = null!;
    public static TenantDbContext TenantDbContext { get; private set; } = null!;
    public static ISmartMeterRepository SmartMeterRepository { get; private set; } = null!;
    public static IUserRepository UserRepository { get; private set; } = null!;
    public static HttpClient HttpClient { get; private set; } = null!;
    public static string AccessToken { get; private set; } = null!;

    [OneTimeSetUp]
    public static async Task OneTimeSetup()
    {
        const int postgresPort = 5432;
        _postgresContainer = new PostgreSqlBuilder()
            .WithImage("postgres:16-bullseye")
            .WithUsername("user")
            .WithPassword("password")
            .WithDatabase("smaiax-db")
            .WithPortBinding(postgresPort, true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(postgresPort))
            .Build();

        await _postgresContainer.StartAsync();
        
        var postgresMappedPublicPort = _postgresContainer.GetMappedPublicPort(postgresPort);
        _webAppFactory = new WebAppFactory(postgresMappedPublicPort);

        HttpClient = _webAppFactory.CreateClient();

        ApplicationDbContext = _webAppFactory.Services.GetRequiredService<ApplicationDbContext>();
        TenantDbContext = _webAppFactory.Services.GetRequiredService<TenantDbContext>();
        SmartMeterRepository = _webAppFactory.Services.GetRequiredService<ISmartMeterRepository>();
        UserRepository = _webAppFactory.Services.GetRequiredService<IUserRepository>();

        var tokenRepository = _webAppFactory.Services.GetRequiredService<ITokenRepository>();
        AccessToken = await tokenRepository.GenerateAccessTokenAsync($"{Guid.NewGuid()}-{Guid.NewGuid()}",
            "3c07065a-b964-44a9-9cdf-fbd49d755ea7", "john.doe@example.com");
    }

    [OneTimeTearDown]
    public static async Task OneTimeTearDown()
    {
        await _postgresContainer.StopAsync();
        await _postgresContainer.DisposeAsync();
        HttpClient.Dispose();
        await _webAppFactory.DisposeAsync();
    }
}