using System.Security.Claims;
using Toodo.Application.Common.Security;

namespace Toodo.API.Services;

public class UserContext(IHttpContextAccessor accessor) : IUserContext
{
    public CurrentUser GetCurrentUser()
    {
        return new CurrentUser(
            accessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier),
            accessor.HttpContext?.User.FindAll(ClaimTypes.Role).Select(x => x.Value).ToList()
        );
    }
}