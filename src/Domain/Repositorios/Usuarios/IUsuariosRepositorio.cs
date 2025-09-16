using Domain.Entities.Usuarios;

namespace Domain.Repositorios.Usuarios;

public interface IUsuariosRepositorio
{
    Task<Usuario?> ObterPorIdAsync(Guid id, CancellationToken ct = default);
    Task<bool> UsuarioPossuiPermissaoAsync(Guid usuarioId, string permissaoCodigo, CancellationToken ct = default);
}
