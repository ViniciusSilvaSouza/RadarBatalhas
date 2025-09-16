using Domain.Entities.Eventos;

namespace Domain.Repositorios.Eventos;

public interface IChaveamentoRepositorio
{
    Task<Chaveamento?> ObterPorEventoAsync(Guid eventoId, CancellationToken ct = default);
    Task AdicionarAsync(Chaveamento chave, CancellationToken ct = default);
    Task<bool> AlgumConfrontoComVencedorAsync(Guid chaveamentoId, CancellationToken ct = default);
    Task TravarAsync(Guid chaveamentoId, DateTime travadoEm, CancellationToken ct = default);
}
