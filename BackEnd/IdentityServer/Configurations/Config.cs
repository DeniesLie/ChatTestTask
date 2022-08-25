using IdentityModel;
using IdentityServer4.Models;

namespace IdentityServer.Configurations;

public static class Config
{
    public static IEnumerable<IdentityResource> GetIdentityResources()
        =>  new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiResource> GetApiResources()
        => new List<ApiResource>()
        {
            new ApiResource("chatApi", "Chat API")
            {
                Scopes = {"apiAccess"}
            } 
        };
    
    public static IEnumerable<ApiScope> GetApiScopes()
        =>  new[]
        {
            new ApiScope("apiAccess", "Access chat Web Api")
        };

    public static IEnumerable<Client> GetClients()
        => new[]
        {
            // angular client
            new Client()
            {
                ClientId = "angular_client",
                ClientName = "Angular Client",
                AllowedScopes = {"apiAccess", "openid", "profile"},
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowOfflineAccess = true, // enable refresh tokens
                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                ClientSecrets = {new Secret("angular_client_secret".ToSha256())},
                AllowedCorsOrigins = new [] {"http://localhost:4200"},
                AccessTokenLifetime = 600,
            }
        };
}