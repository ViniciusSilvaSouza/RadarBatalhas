using Microsoft.AspNetCore.Authorization;

namespace Api.Auth;

public sealed class PermissionRequirement : IAuthorizationRequirement
{
    public string Codigo { get; }
    public PermissionRequirement(string codigo)
    {
        Codigo = codigo;
    }
}
