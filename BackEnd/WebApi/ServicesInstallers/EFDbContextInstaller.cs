using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace WebApi.ServicesInstallers;

public static class EFDbContextInstaller
{
    public static void AddEFDbContext(
        this IServiceCollection services, 
        IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(opts =>{ 
            opts.UseSqlServer(config.GetConnectionString("Default"));
        });
    }
}