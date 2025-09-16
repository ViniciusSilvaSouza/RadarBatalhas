using Domain.Entities.Usuarios;

namespace Domain.Repositorios.Usuarios;

public interface IUsuariosRepositorio
{
    Task<Usuario?> ObterPorIdAsync(Guid id, CancellationToken ct = default);
    Task<bool> UsuarioPossuiPermissaoAsync(Guid usuarioId, string permissaoCodigo, CancellationToken ct = default);

    Task<Papel?> ObterPapelPorNomeAsync(string nome, CancellationToken ct = default);
    Task UpsertUsuarioAsync(Usuario usuario, CancellationToken ct = default);
    Task RemoverTodosPapeisAsync(Guid usuarioId, CancellationToken ct = default);
    Task AdicionarUsuarioPapelAsync(Guid usuarioId, Guid papelId, CancellationToken ct = default);
}
