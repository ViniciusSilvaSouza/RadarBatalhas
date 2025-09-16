using Application.DTOs.Eventos;
using AutoMapper;
using Domain.Repositorios.Eventos;

namespace Application.UseCases.Evento;

public class ListarProximosEventosUseCase(IEventoRepositorio repo, IMapper mapper)
{
    private readonly IEventoRepositorio _repo = repo;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<EventoDto>> ExecuteAsync(CancellationToken ct = default)
    {
        var eventos = await _repo.ListarAbertosAsync(DateTime.UtcNow, ct);
        return _mapper.Map<IEnumerable<EventoDto>>(eventos);
    }
}
