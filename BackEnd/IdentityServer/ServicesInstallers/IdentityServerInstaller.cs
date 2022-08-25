using IdentityServer.Configurations;
using IdentityServer.Services;

namespace IdentityServer.ServicesInstallers;

public static class IdentityServerInstaller
{
    public static void AddIdentityServerWithConfigurations(this IServiceCollection services, IConfiguration config)
    {
        services.AddIdentityServer()
            .AddDeveloperSigningCredential() // TODO : change on production
            .AddInMemoryIdentityResources(Config.GetIdentityResources())
            .AddInMemoryApiResources(Config.GetApiResources())
            .AddInMemoryApiScopes(Config.GetApiScopes())
            .AddInMemoryClients(Config.GetClients())
            .AddUserStore(config);
    }
}