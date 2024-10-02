using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SMAIAXBackend.Application.DTOs;
using SMAIAXBackend.Application.Exceptions;
using SMAIAXBackend.Application.Services.Interfaces;
using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Repositories;

namespace SMAIAXBackend.Application.Services.Implementations;

public class UserService(
    IUserRepository userRepository,
    ITokenService tokenService,
    UserManager<IdentityUser> userManager,
    ILogger<UserService> logger) : IUserService
{
    public async Task<Guid> RegisterAsync(RegisterDto registerDto)
    {
        var userId = userRepository.NextIdentity();
        var identityUser = new IdentityUser
        {
            Id = userId.Id.ToString(), UserName = registerDto.Email, Email = registerDto.Email
        };

        var result = await userManager.CreateAsync(identityUser, registerDto.Password);
    
        if (!result.Succeeded)
        {
            var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
            logger.LogError("Registration failed with the following errors: {ErrorMessages}", errorMessages);
            throw new RegistrationException(errorMessages);
        }
        
        var domainUser = User.Create(userId, registerDto.Name, registerDto.Address, registerDto.Email);
        await userRepository.AddAsync(domainUser);

        return userId.Id;
    }

    public async Task<string> LoginAsync(LoginDto loginDto)
    {
        var user = await userManager.FindByNameAsync(loginDto.Username);

        if (user == null)
        {
            logger.LogError("User with `{Username}` not found.", loginDto.Username);
            throw new InvalidLoginException();
        }
        
        var isPasswordCorrect  = await userManager.CheckPasswordAsync(user, loginDto.Password);

        if (!isPasswordCorrect)
        {
            // TODO: Increment AccessFailedCount and lock user for a configured amount of time?
            logger.LogError("Invalid password for user `{Username}`.", loginDto.Username);
            throw new InvalidLoginException();
        }
        
        // To avoid the null reference warning we access the username like this
        // although there will definitely be a username
        var userName = user.UserName ?? string.Empty;
        var accessToken = await tokenService.GenerateAccessTokenAsync(user.Id, userName);

        return accessToken;
    }
}