using Application.Services;
using Core.Interfaces.Services;

namespace WebApi.ServicesInstallers;

public static class CustomServicesInstaller
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IChatroomService, ChatroomService>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IUserService, UserService>();
    }
}