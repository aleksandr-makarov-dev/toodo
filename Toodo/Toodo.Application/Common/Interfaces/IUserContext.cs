namespace Toodo.Application.Common.Security;

public interface IUserContext
{
    CurrentUser GetCurrentUser();
}