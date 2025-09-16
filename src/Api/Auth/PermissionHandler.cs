using System.Security.Claims;
using System.Text.Json;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Api.Auth;

public sealed class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly RadarDbContext _db;

    private static readonly Dictionary<string, string[]> PolicyRoleMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["noticias.publicar"] = new[] { "ADMINISTRADOR" },
        ["eventos.criar"]      = new[] { "ADMINISTRADOR", "ORGANIZADOR" }
    };

    public PermissionHandler(RadarDbContext db) => _db = db;

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (!TryGetSubject(context.User, out var userId))
            return;

        var hasDb = await _db.Usuarios
            .Where(u => u.Id == userId)
            .SelectMany(u => u.UsuarioPapeis)
            .Select(up => up.Papel)
            .SelectMany(p => p.PapelPermissoes)
            .AnyAsync(pp => pp.Permissao.Codigo == requirement.Codigo);

        if (hasDb)
        {
            context.Succeed(requirement);
            return;
        }

        if (PolicyRoleMap.TryGetValue(requirement.Codigo, out var rolesAceitas))
        {
            var rolesToken = ExtractRoles(context.User);
            if (rolesToken.Overlaps(rolesAceitas, StringComparer.OrdinalIgnoreCase))
            {
                context.Succeed(requirement);
                return;
            }
        }
    }

    private static bool TryGetSubject(ClaimsPrincipal user, out Guid userId)
    {
        userId = Guid.Empty;
        var sub = user.FindFirst("sub")?.Value
                  ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                  ?? user.FindFirst("nameid")?.Value;
        return !string.IsNullOrWhiteSpace(sub) && Guid.TryParse(sub, out userId);
    }

    private static HashSet<string> ExtractRoles(ClaimsPrincipal user)
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

        return roles;
    }
}

internal static class SetExtensions
{
    public static bool Overlaps(this HashSet<string> set, IEnumerable<string> other, IEqualityComparer<string> cmp)
    {
        foreach (var v in other)
        {
            if (set.Contains(v)) return true;
            if (cmp != null && set.Any(s => cmp.Equals(s, v))) return true;
        }
        return false;
    }
}
