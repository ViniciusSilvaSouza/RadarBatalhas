using Domain.Entities.Noticia;
namespace Domain.Repositorios.Noticias;

public interface INoticiasRepositorio
{
    Task AdicionarAsync(Noticia n, CancellationToken ct = default);
    Task<Noticia?> ObterPorIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Noticia>> ListarPublicadasAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Noticia>> ListarRascunhosAsync(CancellationToken ct = default);
    Task AtualizarAsync(Noticia n, CancellationToken ct = default);
}
