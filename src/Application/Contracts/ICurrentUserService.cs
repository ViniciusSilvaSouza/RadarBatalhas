using System.Security.Claims;

namespace Application.Contracts;

public interface ICurrentUserService
{
    bool TryGetSubject(ClaimsPrincipal user, out Guid userId);
    IReadOnlyCollection<string> GetRoles(ClaimsPrincipal user);
}
