using Application.DTOs.Common;
using Application.DTOs.Usuarios;
using Domain.Compartilhado.Contracts;
using Domain.Repositorios.Usuarios;

namespace Application.UseCases.Usuarios;

public class CriarOuAtualizarUsuarioAdminUseCase(IUsuariosRepositorio repo, IUnitOfWork uow)
{
    private readonly IUsuariosRepositorio _repo = repo;
    private readonly IUnitOfWork _uow = uow;

    public async Task<Envelope<object>> ExecuteAsync(CriarUsuarioRequest req, CancellationToken ct = default)
    {
        var papel = await _repo.ObterPapelPorNomeAsync(req.Papel, ct);
        if (papel is null)
            throw new InvalidOperationException($"Papel inválido: {req.Papel}");

        var usuario = await _repo.ObterPorIdAsync(req.Id, ct) ?? new Domain.Entities.Usuarios.Usuario { Id = req.Id };
        usuario.Nome = req.Nome;
        usuario.Email = req.Email;
        usuario.EhOrganizador = string.Equals(papel.Nome, "ORGANIZADOR", StringComparison.OrdinalIgnoreCase) || string.Equals(papel.Nome, "ADMINISTRADOR", StringComparison.OrdinalIgnoreCase);
        usuario.EhMc = string.Equals(papel.Nome, "MC", StringComparison.OrdinalIgnoreCase);

        await _repo.UpsertUsuarioAsync(usuario, ct);

        await _repo.RemoverTodosPapeisAsync(req.Id, ct);
        await _repo.AdicionarUsuarioPapelAsync(req.Id, papel.Id, ct);

        await _uow.SaveChangesAsync(ct);

        return Envelope<object>.Ok(new { usuario.Id, usuario.Nome, usuario.Email, Papel = papel.Nome, usuario.EhOrganizador, usuario.EhMc });
    }
}
