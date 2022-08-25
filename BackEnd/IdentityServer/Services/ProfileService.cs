using System.Security.Claims;
using Core.Interfaces.Services;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace IdentityServer.Services;

public class ProfileService : IProfileService
{
    private readonly IUserService _userService;

    public ProfileService(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var sub = context.Subject.GetSubjectId();
        
        var user = await _userService.GetBySubject(sub);

        if (user is not null)
        {
            var claims = new List<Claim>()
            {
                new Claim("username", user.Username!)
            };

            context.IssuedClaims = claims;
        }
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var sub = context.Subject.GetSubjectId();
        var user = await _userService.GetBySubject(sub);

        context.IsActive = user?.IsActive ?? false;
    }
}