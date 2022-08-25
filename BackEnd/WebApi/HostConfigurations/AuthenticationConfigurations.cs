using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.HostConfigurations;

public static class AuthenticationConfigurations
{
    public static void AddAuthenticationWithIdentityServer(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.Authority = config["AuthProvider:Url"];
            options.Audience = "chatApi";
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ClockSkew = TimeSpan.Zero
            };
        });
    }
}