using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.Interfaces.Services;

public interface IUserService
{
    Task<User?> GetByIdAsync(int userId);
    Task<User?> GetBySubject(string sub);
    Task<User?> GetByUsernameAsync(string username);
    Task<bool> ValidateCredentialsAsync(string username, string password);
}