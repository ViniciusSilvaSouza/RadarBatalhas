using Application.DTOs.Eventos;
using AutoMapper;
using Domain.Repositorios.Eventos;

namespace Application.UseCases.Evento;

public class ObterEventoPorIdUseCase(IEventoRepositorio repo, IMapper mapper)
{
    private readonly IEventoRepositorio _repo = repo;
    private readonly IMapper _mapper = mapper;

    public async Task<EventoDto?> ExecuteAsync(Guid id, CancellationToken ct = default)
    {
        var e = await _repo.ObterPorIdAsync(id, ct);
        return e is null ? null : _mapper.Map<EventoDto>(e);
    }
}
