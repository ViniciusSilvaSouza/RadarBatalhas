using Domain.Repositorios.Ranking;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Radar.Infrastructure.Repositories;

public class RankingLeituraRepository : IRankingLeituraRepositorio
{
    private readonly RadarDbContext _db;
    public RankingLeituraRepository(RadarDbContext db) => _db = db;

    public async Task<IReadOnlyList<(Guid UsuarioMcId, int Vitorias)>> ObterRankingAsync(CancellationToken ct = default)
    {
        var query = await _db.Vitorias
            .GroupBy(v => v.UsuarioMcId)
            .Select(g => new { UsuarioMcId = g.Key, Vitorias = g.Count() })
            .OrderByDescending(x => x.Vitorias)
            .ToListAsync(ct);
        return query.Select(x => (x.UsuarioMcId, x.Vitorias)).ToList();
    }
}
