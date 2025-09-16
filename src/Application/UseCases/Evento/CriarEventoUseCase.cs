using Application.DTOs.Eventos;
using AutoMapper;
using Domain.Compartilhado.Contracts;
using Domain.Enums;
using Domain.Repositorios.Eventos;

namespace Application.UseCases.Evento;

public class CriarEventoUseCase(IEventoRepositorio repo, IUnitOfWork uow, IMapper mapper)
{
    private readonly IEventoRepositorio _repo = repo;
    private readonly IUnitOfWork _uow = uow;
    private readonly IMapper _mapper = mapper;

    public async Task<EventoDto> ExecuteAsync(CriarEventoRequest request, Guid organizadorId, CancellationToken ct = default)
    {
        var e = new Domain.Entities.Eventos.Evento
        {
            Nome = request.Nome,
            Data = request.Data,
            NomeLocal = request.NomeLocal,
            IdStatus = StatusEvento.RASCUNHO,
            OrganizadorId = organizadorId
        };
        await _repo.AdicionarAsync(e, ct);
        await _uow.SaveChangesAsync(ct);
        return _mapper.Map<EventoDto>(e);
    }
}
