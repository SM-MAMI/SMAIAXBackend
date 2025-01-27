using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.ValueObjects;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;
using SMAIAXBackend.Infrastructure.EntityConfigurations;

namespace SMAIAXBackend.Infrastructure.DbContexts;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<User> DomainUsers { get; init; }
    public DbSet<RefreshToken> RefreshTokens { get; init; }
    public DbSet<Tenant> Tenants { get; init; }
    public DbSet<Contract> Contracts { get; init; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseCamelCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new DomainUserConfiguration());
        builder.ApplyConfiguration(new RefreshTokenConfiguration());
        builder.ApplyConfiguration(new TenantConfiguration());
        builder.ApplyConfiguration(new ContractConfiguration());

        // Place Identity tables in the "auth" schema
        builder.Entity<IdentityUser>(entity => entity.ToTable(name: "AspNetUsers", schema: "auth"));
        builder.Entity<IdentityRole>(entity => entity.ToTable(name: "AspNetRoles", schema: "auth"));
        builder.Entity<IdentityUserRole<string>>(entity => entity.ToTable("AspNetUserRoles", schema: "auth"));
        builder.Entity<IdentityUserClaim<string>>(entity => entity.ToTable("AspNetUserClaims", schema: "auth"));
        builder.Entity<IdentityUserLogin<string>>(entity => entity.ToTable("AspNetUserLogins", schema: "auth"));
        builder.Entity<IdentityRoleClaim<string>>(entity => entity.ToTable("AspNetRoleClaims", schema: "auth"));
        builder.Entity<IdentityUserToken<string>>(entity => entity.ToTable("AspNetUserTokens", schema: "auth"));
    }

    public async Task SeedTestData()
    {
        var hasher = new PasswordHasher<IdentityUser>();

        var johnDoeUserId = new UserId(Guid.Parse("3c07065a-b964-44a9-9cdf-fbd49d755ea7"));
        const string johnDoeUserName = "johndoe";
        const string johnDoeEmail = "john.doe@example.com";
        const string johnDoePassword = "P@ssw0rd";
        const string johnDoeTenantDatabase = "tenant_1_db";

        var johnDoeTestUser = new IdentityUser
        {
            Id = johnDoeUserId.Id.ToString(),
            UserName = johnDoeUserName,
            NormalizedUserName = johnDoeUserName!.ToUpper(),
            Email = johnDoeEmail,
            NormalizedEmail = johnDoeEmail!.ToUpper(),
        };
        var johnDoePasswordHash = hasher.HashPassword(johnDoeTestUser, johnDoePassword!);
        johnDoeTestUser.PasswordHash = johnDoePasswordHash;

        var johnDoeTenant = Tenant.Create(new TenantId(Guid.NewGuid()), "tenant_1_role", johnDoeTenantDatabase!);
        var johnDoeDomainUser = User.Create(johnDoeUserId, new Name("John", "Doe"), johnDoeUserName, johnDoeEmail,
            johnDoeTenant.Id);

        var janeDoeUserId = new UserId(Guid.Parse("4d07065a-b964-44a9-9cdf-fbd49d755ea8"));
        const string janeDoeUserName = "janedoe";
        const string janeDoeEmail = "jane.doe@example.com";
        const string janeDoePassword = "P@ssw0rd";
        const string janeDoeTenantDatabase = "tenant_2_db";

        var janeDoeTestUser = new IdentityUser
        {
            Id = janeDoeUserId.Id.ToString(),
            UserName = janeDoeUserName,
            NormalizedUserName = janeDoeUserName!.ToUpper(),
            Email = janeDoeEmail,
            NormalizedEmail = janeDoeEmail!.ToUpper(),
        };
        var janeDoePasswordHash = hasher.HashPassword(janeDoeTestUser, janeDoePassword!);
        janeDoeTestUser.PasswordHash = janeDoePasswordHash;

        var janeDoeTenant = Tenant.Create(new TenantId(Guid.NewGuid()), "tenant_2_role", janeDoeTenantDatabase!);
        var janeDoeDomainUser = User.Create(janeDoeUserId, new Name("Jane", "Doe"), janeDoeUserName, janeDoeEmail,
            janeDoeTenant.Id);

        var maxMustermannUserId = new UserId(Guid.Parse("5e07065a-b964-44a9-9cdf-fbd49d755ea9"));
        const string maxMustermannUserName = "maxmustermann";
        const string maxMustermannEmail = "max.mustermann@example.com";
        const string maxMustermannPassword = "P@ssw0rd";
        const string maxMustermannTenantDatabase = "tenant_3_db";

        var maxMustermannTestUser = new IdentityUser
        {
            Id = maxMustermannUserId.Id.ToString(),
            UserName = maxMustermannUserName,
            NormalizedUserName = maxMustermannUserName!.ToUpper(),
            Email = maxMustermannEmail,
            NormalizedEmail = maxMustermannEmail!.ToUpper(),
        };
        var maxMustermannPasswordHash = hasher.HashPassword(maxMustermannTestUser, maxMustermannPassword!);
        maxMustermannTestUser.PasswordHash = maxMustermannPasswordHash;

        var maxMustermannTenant =
            Tenant.Create(new TenantId(Guid.NewGuid()), "tenant_3_role", maxMustermannTenantDatabase!);
        var maxMustermannDomainUser = User.Create(maxMustermannUserId, new Name("Max", "Mustermann"),
            maxMustermannUserName, maxMustermannEmail, maxMustermannTenant.Id);

        await Users.AddAsync(johnDoeTestUser);
        await Tenants.AddAsync(johnDoeTenant);
        await DomainUsers.AddAsync(johnDoeDomainUser);

        await Users.AddAsync(janeDoeTestUser);
        await Tenants.AddAsync(janeDoeTenant);
        await DomainUsers.AddAsync(janeDoeDomainUser);

        await Users.AddAsync(maxMustermannTestUser);
        await Tenants.AddAsync(maxMustermannTenant);
        await DomainUsers.AddAsync(maxMustermannDomainUser);

        await SaveChangesAsync();
    }
}