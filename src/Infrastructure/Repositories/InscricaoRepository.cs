using Domain.Entities.Eventos;
using Domain.Repositorios.Eventos;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Radar.Infrastructure.Repositories;

public class InscricaoRepository : IInscricaoRepositorio
{
    private readonly RadarDbContext _db;
    public InscricaoRepository(RadarDbContext db) => _db = db;

    public async Task<bool> ExisteInscricaoAsync(Guid eventoId, Guid usuarioMcId, CancellationToken ct = default)
        => await _db.Inscricoes.AnyAsync(i => i.EventoId == eventoId && i.UsuarioMcId == usuarioMcId, ct);

    public async Task AdicionarAsync(InscricaoEvento insc, CancellationToken ct = default)
        => await _db.Inscricoes.AddAsync(insc, ct);

    public async Task<IReadOnlyList<InscricaoEvento>> ListarPorEventoAsync(Guid eventoId, CancellationToken ct = default)
        => await _db.Inscricoes.AsNoTracking().Where(i => i.EventoId == eventoId).ToListAsync(ct);
}
