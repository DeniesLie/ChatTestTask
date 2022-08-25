using Application.Services;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using DataAccess;
using DataAccess.Repositories;
using IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.ServicesInstallers;

public static class UserStoreInstaller
{
    public static IIdentityServerBuilder AddUserStore(this IIdentityServerBuilder builder, IConfiguration config)
    {
        builder.Services.AddDbContext<AppDbContext>(opts =>
            opts.UseSqlServer(config.GetConnectionString("Default")));
        
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.AddProfileService<ProfileService>();
        builder.AddResourceOwnerValidator<ResourceOwnerValidator>();

        return builder;
    }
}