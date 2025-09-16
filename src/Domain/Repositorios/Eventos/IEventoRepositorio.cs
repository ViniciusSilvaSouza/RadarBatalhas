using Domain.Entities.Eventos;

namespace Domain.Repositorios.Eventos;

public interface IEventoRepositorio
{
    Task<Evento?> ObterPorIdAsync(Guid id, CancellationToken ct = default);
    Task AdicionarAsync(Evento e, CancellationToken ct = default);
    Task<IEnumerable<Evento>> ListarAbertosAsync(DateTime hoje, CancellationToken ct = default);
    Task<IEnumerable<Evento>> ListarPorOrganizadorAsync(Guid organizadorId, CancellationToken ct = default);
}
