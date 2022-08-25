using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using DataAccess.Repositories;

namespace WebApi.ServicesInstallers;

public static class RepositoriesIntaller
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IChatroomRepository, ChatroomRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IUserChatroomRepository, UserChatroomRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}