
using FluentValidation.AspNetCore;
using WebApi.Validators;

namespace WebApi.HostConfigurations;

public static class FluentValidatiorConfiguration
{
    public static void AddFluentValidationWithValidatorsInAssembly(this IServiceCollection services)
    {
        services.AddFluentValidation(config =>
        {
            config.RegisterValidatorsFromAssemblyContaining<MessageSendToChatroomValidator>();
        });
    }
}