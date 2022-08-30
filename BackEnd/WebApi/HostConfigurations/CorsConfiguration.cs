namespace WebApi.HostConfigurations;

public static class CorsConfiguration
{
    public static void AddCorsWithConfiguration(
        this IServiceCollection services, 
        IConfiguration config, 
        IWebHostEnvironment env)
    {
        services.AddCors(opts =>
        {
            opts.AddPolicy(name: "AngularClientPolicy", policy =>
            {
                policy.WithOrigins(config["AngularClient:Url"]);
                policy.WithHeaders("*");
                policy.WithMethods("*");
                policy.AllowCredentials();
            });
        });
    }
}