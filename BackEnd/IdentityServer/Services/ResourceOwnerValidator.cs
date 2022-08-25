using Core.Interfaces.Services;
using IdentityModel;
using IdentityServer4.Validation;

namespace IdentityServer.Services;

public class ResourceOwnerValidator : IResourceOwnerPasswordValidator
{
    private readonly IUserService _userService;

    public ResourceOwnerValidator(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        if (await _userService.ValidateCredentialsAsync(context.UserName, context.Password))
        {
            var user = await _userService.GetByUsernameAsync(context.UserName);
            var sub = user!.Id.ToString();
            context.Result = 
                new GrantValidationResult(sub, OidcConstants.AuthenticationMethods.Password);
        }
    }
}