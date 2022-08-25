using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace WebApi.ServicesInstallers;

public static class ServiceInstaller
{
    public static void InstallServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddAutoMapper(typeof(Program).Assembly);
        services.AddEFDbContext(config);
        services.AddRepositories();
        services.AddCustomServices();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddSignalR();
    }
}