using Domain.Entities.Usuarios;
using Domain.Repositorios.Usuarios;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UsuariosRepository(RadarDbContext db) : IUsuariosRepositorio
{
    private readonly RadarDbContext _db = db;

    public Task<Usuario?> ObterPorIdAsync(Guid id, CancellationToken ct = default)
        => _db.Usuarios.FirstOrDefaultAsync(u => u.Id == id, ct);

    public async Task<bool> UsuarioPossuiPermissaoAsync(Guid usuarioId, string permissaoCodigo, CancellationToken ct = default)
    {
        return await _db.Usuarios
            .Where(u => u.Id == usuarioId)
            .SelectMany(u => u.UsuarioPapeis)
            .Select(up => up.Papel)
            .SelectMany(p => p.PapelPermissoes)
            .AnyAsync(pp => pp.Permissao.Codigo == permissaoCodigo, ct);
    }

    public Task<Papel?> ObterPapelPorNomeAsync(string nome, CancellationToken ct = default)
        => _db.Papeis.FirstOrDefaultAsync(p => p.Nome == nome.ToUpper(), ct);

    public async Task UpsertUsuarioAsync(Usuario usuario, CancellationToken ct = default)
    {
        var exists = await _db.Usuarios.AnyAsync(u => u.Id == usuario.Id, ct);
        if (!exists) _db.Usuarios.Add(usuario);
        else _db.Usuarios.Update(usuario);
    }

    public async Task RemoverTodosPapeisAsync(Guid usuarioId, CancellationToken ct = default)
    {
        var links = await _db.UsuariosPapeis.Where(up => up.UsuarioId == usuarioId).ToListAsync(ct);
        if (links.Count > 0)
        {
            _db.UsuariosPapeis.RemoveRange(links);
        }
    }

    public Task AdicionarUsuarioPapelAsync(Guid usuarioId, Guid papelId, CancellationToken ct = default)
    {
        _db.UsuariosPapeis.Add(new UsuarioPapel { UsuarioId = usuarioId, PapelId = papelId });
        return Task.CompletedTask;
    }
}
