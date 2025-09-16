using System.Security.Claims;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Radar.Domain.Compartilhado.Contracts;

namespace Radar.Infrastructure.Auth;

public class RbacAuthorizationService : IServicoAutorizacao
{
    private readonly RadarDbContext _db;
    public RbacAuthorizationService(RadarDbContext db) => _db = db;

    public async Task DemandAsync(ClaimsPrincipal user, string permissao)
    {
        var sub = user.FindFirst("sub")?.Value; // avoid extension method
        if (string.IsNullOrWhiteSpace(sub)) throw new UnauthorizedAccessException("missing sub claim");
        if (!Guid.TryParse(sub, out var userId)) throw new UnauthorizedAccessException("invalid sub");

        var has = await _db.Usuarios
            .Where(u => u.Id == userId)
            .SelectMany(u => u.UsuarioPapeis)
            .Select(up => up.Papel)
            .SelectMany(p => p.PapelPermissoes)
            .AnyAsync(pp => pp.Permissao.Codigo == permissao);

        if (!has) throw new UnauthorizedAccessException("forbidden");
    }
}
