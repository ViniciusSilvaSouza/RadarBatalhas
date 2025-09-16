using System.Security.Claims;
using Application.Contracts;
using System.Text.Json;

namespace Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    public bool TryGetSubject(ClaimsPrincipal user, out Guid userId)
    {
        userId = Guid.Empty;
        var sub = user.FindFirst("sub")?.Value
                  ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                  ?? user.FindFirst("nameid")?.Value;
        return !string.IsNullOrWhiteSpace(sub) && Guid.TryParse(sub, out userId);
    }

    public IReadOnlyCollection<string> GetRoles(ClaimsPrincipal user)
    {
        var roles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var c in user.Claims.Where(c => c.Type is ClaimTypes.Role or "role" or "roles"))
            roles.Add(c.Value);

        var realmAccessJson = user.FindFirst("realm_access")?.Value;
        if (!string.IsNullOrWhiteSpace(realmAccessJson))
        {
            try
            {
                using var doc = JsonDocument.Parse(realmAccessJson);
                if (doc.RootElement.TryGetProperty("roles", out var arr) && arr.ValueKind == JsonValueKind.Array)
                    foreach (var r in arr.EnumerateArray()) if (r.ValueKind == JsonValueKind.String) roles.Add(r.GetString()!);
            }
            catch { }
        }

        var resourceAccessJson = user.FindFirst("resource_access")?.Value;
        if (!string.IsNullOrWhiteSpace(resourceAccessJson))
        {
            try
            {
                using var doc = JsonDocument.Parse(resourceAccessJson);
                foreach (var clientProp in doc.RootElement.EnumerateObject())
                {
                    if (clientProp.Value.TryGetProperty("roles", out var arr) && arr.ValueKind == JsonValueKind.Array)
                        foreach (var r in arr.EnumerateArray()) if (r.ValueKind == JsonValueKind.String) roles.Add(r.GetString()!);
                }
            }
            catch { }
        }

        return roles.ToArray();
    }
}
