using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using SMAIAXBackend.Application.DTOs;
using SMAIAXBackend.Application.Exceptions;
using SMAIAXBackend.Application.Services.Interfaces;
using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.ValueObjects;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;
using SMAIAXBackend.Domain.Repositories;
using SMAIAXBackend.Domain.Repositories.Transactions;

namespace SMAIAXBackend.Application.Services.Implementations;

public class AuthenticationService(
    ITenantRepository tenantRepository,
    IUserRepository userRepository,
    ITokenRepository tokenRepository,
    UserManager<IdentityUser> userManager,
    ITransactionManager transactionManager,
    ILogger<AuthenticationService> logger) : IAuthenticationService
{
    // TODO: Refactor method
    public async Task<Guid> RegisterAsync(RegisterDto registerDto)
    {
        var userId = userRepository.NextIdentity();

        var tenantId = tenantRepository.NextIdentity();
        var databaseName = $"tenant_{tenantId.Id.ToString().Replace("-", "_")}_db";

        await transactionManager.TransactionScope(async () =>
        {
            var identityUser = new IdentityUser
            {
                Id = userId.Id.ToString(), UserName = registerDto.UserName, Email = registerDto.Email
            };

            var result = await userManager.CreateAsync(identityUser, registerDto.Password);

            if (!result.Succeeded)
            {
                var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
                logger.LogError("Registration failed with the following errors: {ErrorMessages}", errorMessages);
                throw new RegistrationException(errorMessages);
            }

            var tenant = Tenant.Create(tenantId, registerDto.UserName, registerDto.Password, databaseName);
            await tenantRepository.AddAsync(tenant);

            var name = new Name(registerDto.Name.FirstName, registerDto.Name.LastName);
            var domainUser = User.Create(userId, name, registerDto.UserName, registerDto.Email, tenantId);
            await userRepository.AddAsync(domainUser);
        });

        // TODO: How to handle the case if the database creation fails e.g. deleting identity user, domain user and tenant?
        // Needs to be done outside of transaction
        await tenantRepository.CreateDatabaseForTenantAsync(databaseName, registerDto.UserName,
            registerDto.Password);

        return userId.Id;
    }

    public async Task<TokenDto> LoginAsync(LoginDto loginDto)
    {
        var user = await userManager.FindByNameAsync(loginDto.UserName) ??
                   await userManager.FindByEmailAsync(loginDto.UserName);

        if (user == null)
        {
            logger.LogError("User with `{Username}` not found.", loginDto.UserName);
            throw new InvalidLoginException();
        }

        var isPasswordCorrect = await userManager.CheckPasswordAsync(user, loginDto.Password);

        if (!isPasswordCorrect)
        {
            logger.LogError("Invalid password for user `{Username}`.", loginDto.UserName);
            throw new InvalidLoginException();
        }

        // To avoid the null reference warning
        var userName = user.UserName ?? string.Empty;
        var jwtId = tokenRepository.NextIdentity();
        var accessToken = await tokenRepository.GenerateAccessTokenAsync(jwtId.ToString(), user.Id, userName);
        var refreshTokenId = new RefreshTokenId(tokenRepository.NextIdentity());
        var refreshToken = await tokenRepository.GenerateRefreshTokenAsync(refreshTokenId, jwtId.ToString(), user.Id);
        var tokenDto = new TokenDto(accessToken, refreshToken.Token);

        return tokenDto;
    }

    public async Task<TokenDto> RefreshTokensAsync(TokenDto tokenDto)
    {
        var existingRefreshToken = await tokenRepository.GetRefreshTokenByTokenAsync(tokenDto.RefreshToken);

        if (existingRefreshToken == null || !existingRefreshToken.IsValid)
        {
            logger.LogError("Invalid or non-existent refresh token: {RefreshToken}", tokenDto.RefreshToken);
            throw new InvalidTokenException();
        }

        if (existingRefreshToken.ExpiresAt < DateTime.UtcNow)
        {
            existingRefreshToken.Invalidate();
            await tokenRepository.UpdateAsync(existingRefreshToken);
            logger.LogError("Expired refresh token: {RefreshToken}, Expired at: {ExpiresAt}", tokenDto.RefreshToken,
                existingRefreshToken.ExpiresAt);
            throw new InvalidTokenException();
        }

        var isAccessTokenValid = tokenRepository.ValidateAccessToken(tokenDto.AccessToken, existingRefreshToken.UserId,
            existingRefreshToken.JwtTokenId);

        if (!isAccessTokenValid)
        {
            existingRefreshToken.Invalidate();
            await tokenRepository.UpdateAsync(existingRefreshToken);
            logger.LogError("Invalid access token for refresh token: {RefreshToken}, User ID: {UserId}",
                tokenDto.RefreshToken, existingRefreshToken.UserId);
            throw new InvalidTokenException();
        }

        var identityUser = await userManager.FindByIdAsync(existingRefreshToken.UserId.ToString()!);

        if (identityUser == null)
        {
            logger.LogError("User not found for ID: {UserId}", existingRefreshToken.UserId.ToString());
            throw new InvalidTokenException();
        }

        existingRefreshToken.Invalidate();
        await tokenRepository.UpdateAsync(existingRefreshToken);

        var jwtId = tokenRepository.NextIdentity();
        var refreshTokenId = new RefreshTokenId(tokenRepository.NextIdentity());
        var newRefreshToken = await tokenRepository.GenerateRefreshTokenAsync(refreshTokenId, jwtId.ToString(),
            identityUser.Id);
        var newAccessToken = await tokenRepository.GenerateAccessTokenAsync(jwtId.ToString(),
            identityUser.Id, identityUser.UserName!);
        var refreshedTokens = new TokenDto(newAccessToken, newRefreshToken.Token);

        return refreshedTokens;
    }

    public async Task LogoutAsync(string refreshToken)
    {
        var token = await tokenRepository.GetRefreshTokenByTokenAsync(refreshToken);

        if (token == null || !token.IsValid)
        {
            logger.LogWarning("Invalid or already used refresh token: '{RefreshToken}'.", refreshToken);
            throw new UnauthorizedAccessException("Invalid or already used refresh token.");
        }

        token.Invalidate();
        await tokenRepository.UpdateAsync(token);
    }
}