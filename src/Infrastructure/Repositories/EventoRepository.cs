using Domain.Entities.Eventos;
using Domain.Enums;
using Domain.Repositorios.Eventos;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EventoRepository(RadarDbContext db) : IEventoRepositorio
{
    private readonly RadarDbContext _db = db;

    public async Task<Evento?> ObterPorIdAsync(Guid id, CancellationToken ct = default)
        => await _db.Eventos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task AdicionarAsync(Evento e, CancellationToken ct = default)
        => await _db.Eventos.AddAsync(e, ct);

    public Task<IEnumerable<Evento>> ListarAbertosAsync(DateTime hoje, CancellationToken ct = default)
    {
        var q = _db.Eventos.AsNoTracking().Where(x => x.IdStatus == StatusEvento.INSCRICOES_ABERTAS && x.Data >= hoje);
        return Task.FromResult(q.AsEnumerable());
    }

    public Task<IEnumerable<Evento>> ListarPorOrganizadorAsync(Guid organizadorId, CancellationToken ct = default)
    {
        var q = _db.Eventos.AsNoTracking().Where(x => x.OrganizadorId == organizadorId).OrderByDescending(x => x.Data);
        return Task.FromResult(q.AsEnumerable());
    }
}
