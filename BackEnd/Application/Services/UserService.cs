using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepo;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(IUserRepository userRepo, IPasswordHasher<User> passwordHasher)
    {
        _userRepo = userRepo;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<User?> GetByIdAsync(int userId)
    {
        return await _userRepo.GetFirstOrDefaultAsync(filter: u => u.Id == userId);
    }

    public async Task<User?> GetBySubject(string sub)
    {
        if (Int32.TryParse(sub, out int userId))
            return await GetByIdAsync(userId);

        return null;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _userRepo.GetFirstOrDefaultAsync(filter: u => u.Username == username);
    }

    public async Task<bool> ValidateCredentialsAsync(string username, string password)
    {
        var user = await GetByUsernameAsync(username);

        if (user is null)
            return false;

        var verificationResult = 
            _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

        return verificationResult == PasswordVerificationResult.Success;
    }
}