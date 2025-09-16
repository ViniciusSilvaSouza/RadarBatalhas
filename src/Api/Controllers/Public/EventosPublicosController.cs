using Application.DTOs.Common;
using Application.DTOs.Eventos;
using Application.UseCases.Evento;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Public;

[ApiController]
[Route("api/v1/public/eventos")] 
public class EventosPublicosController(ListarProximosEventosUseCase listar, ObterEventoPorIdUseCase obter) : ControllerBase
{
    private readonly ListarProximosEventosUseCase _listar = listar;
    private readonly ObterEventoPorIdUseCase _obter = obter;

    [HttpGet]
    [ProducesResponseType(typeof(Envelope<IEnumerable<EventoDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Envelope<IEnumerable<EventoDto>>>> GetAll(CancellationToken ct)
    {
        var dto = await _listar.ExecuteAsync(ct);
        return Ok(Envelope<IEnumerable<EventoDto>>.Ok(dto));
    }


    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Envelope<EventoDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Envelope<EventoDto>>> ObterPorId(Guid id, CancellationToken ct)
    {
        var dto = await _obter.ExecuteAsync(id, ct);
        if (dto is null) return NotFound();
        return Ok(Envelope<EventoDto>.Ok(dto));
    }
}
