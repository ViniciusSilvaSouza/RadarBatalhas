using Application.DTOs.Eventos;
using AutoMapper;
using Domain.Repositorios.Eventos;

namespace Application.UseCases.Evento;

public class ListarEventosDoOrganizadorUseCase(IEventoRepositorio repo, IMapper mapper)
{
    private readonly IEventoRepositorio _repo = repo;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<EventoDto>> ExecuteAsync(Guid organizadorId, CancellationToken ct = default)
    {
        var eventos = await _repo.ListarPorOrganizadorAsync(organizadorId, ct);
        return _mapper.Map<IEnumerable<EventoDto>>(eventos);
    }
}
