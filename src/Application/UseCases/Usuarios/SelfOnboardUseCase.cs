using Application.DTOs.Common;
using Application.DTOs.Usuarios;
using Domain.Compartilhado.Contracts;
using Domain.Entities.Usuarios;
using Domain.Repositorios.Usuarios;

namespace Application.UseCases.Usuarios;

public class SelfOnboardUseCase(IUsuariosRepositorio repo, IUnitOfWork uow)
{
    private readonly IUsuariosRepositorio _repo = repo;
    private readonly IUnitOfWork _uow = uow;

    public async Task<Envelope<object>> ExecuteAsync(Guid userId, SelfOnboardRequest req, IEnumerable<string> rolesToken, CancellationToken ct = default)
    {
        var usuario = await _repo.ObterPorIdAsync(userId, ct) ?? new Usuario { Id = userId };
        if (!string.IsNullOrWhiteSpace(req.Nome)) usuario.Nome = req.Nome;
        if (!string.IsNullOrWhiteSpace(req.Email)) usuario.Email = req.Email;

        var isOrg = rolesToken.Contains("ORGANIZADOR", StringComparer.OrdinalIgnoreCase) || rolesToken.Contains("ADMINISTRADOR", StringComparer.OrdinalIgnoreCase);
        var isMc  = rolesToken.Contains("MC", StringComparer.OrdinalIgnoreCase);
        usuario.EhOrganizador = isOrg;
        usuario.EhMc = isMc;

        await _repo.UpsertUsuarioAsync(usuario, ct);

        await _repo.RemoverTodosPapeisAsync(userId, ct);
        var papelNome = isOrg ? "ORGANIZADOR" : isMc ? "MC" : "VISUALIZADOR";
        var papel = await _repo.ObterPapelPorNomeAsync(papelNome, ct);
        if (papel is not null)
            await _repo.AdicionarUsuarioPapelAsync(userId, papel.Id, ct);

        await _uow.SaveChangesAsync(ct);

        return Envelope<object>.Ok(new { usuario.Id, usuario.Nome, usuario.Email, usuario.EhOrganizador, usuario.EhMc, Papel = papelNome });
    }
}
