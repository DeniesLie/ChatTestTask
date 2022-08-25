using WebApi.Middleware;

namespace WebApi.HostConfigurations;

public static class ExceptionMiddlewareExtension
{
    public static void ConfigureCustomExceptionMiddleware(this WebApplication app) 
    { 
        app.UseMiddleware<ExceptionMiddleware>(); 
    }
}