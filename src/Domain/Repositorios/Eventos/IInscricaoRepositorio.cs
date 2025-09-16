using Domain.Entities.Eventos;

namespace Domain.Repositorios.Eventos;

public interface IInscricaoRepositorio
{
    Task<bool> ExisteInscricaoAsync(Guid eventoId, Guid usuarioMcId, CancellationToken ct = default);
    Task AdicionarAsync(InscricaoEvento insc, CancellationToken ct = default);
    Task<IReadOnlyList<InscricaoEvento>> ListarPorEventoAsync(Guid eventoId, CancellationToken ct = default);
}
