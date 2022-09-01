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
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.SetIsOriginAllowed(origin => true);
                policy.AllowCredentials();
            });
        });
    }
}