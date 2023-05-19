using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Security.Claims;

namespace Charisma.Presentation.Controllers;

[Authorize]
[ApiController]
public class AppBaseController : ControllerBase
{
    public string UserId { get; private set; }

    public AppBaseController(IHttpContextAccessor httpContextAccessor)
    {
        Claim userClaim = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier));

        if (userClaim is null)
            throw new AuthenticationException();

        UserId = userClaim.Value;
    }
}