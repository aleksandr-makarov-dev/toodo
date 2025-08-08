namespace Toodo.Application.Common.Security;

public class CurrentUser(string? id, List<string>? roles)
{
    public string? Id { get; } = id;
    public List<string>? Roles { get; } = roles;
}