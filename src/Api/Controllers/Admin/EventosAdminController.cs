using Api.Controllers.Public;
using Application.DTOs.Common;
using Application.DTOs.Eventos;
using Application.UseCases.Evento;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Radar.Domain.Compartilhado.Contracts;

namespace Api.Controllers.Admin;

[ApiController]
[Route("api/v1/admin/eventos")]
[Authorize]
public class EventosAdminController(CriarEventoUseCase criar, IServicoAutorizacao authz) : ControllerBase
{
    private readonly CriarEventoUseCase _criar = criar;
    private readonly IServicoAutorizacao _authz = authz;

    [HttpPost]
    [ProducesResponseType(typeof(Envelope<EventoDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Envelope<EventoDto>>> Criar([FromBody] CriarEventoRequest req, CancellationToken ct)
    {
        await _authz.DemandAsync(User, "eventos.criar");
        var dto = await _criar.ExecuteAsync(req, ct);
        // retorna 201 com Location apontando para GET público por id
        return CreatedAtAction(
            actionName: nameof(EventosPublicosController.ObterPorId),
            controllerName: "EventosPublicos",
            routeValues: new { id = dto.Id },
            value: Envelope<EventoDto>.Ok(dto)
        );
    }
}
