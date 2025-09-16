namespace Domain.Repositorios.Ranking;

public interface IRankingLeituraRepositorio
{
    Task<IReadOnlyList<(Guid UsuarioMcId, int Vitorias)>> ObterRankingAsync(CancellationToken ct = default);
}
