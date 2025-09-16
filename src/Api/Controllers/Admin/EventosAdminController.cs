using Api.Controllers.Public;
using Application.DTOs.Common;
using Application.DTOs.Eventos;
using Application.UseCases.Evento;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Contracts;

namespace Api.Controllers.Admin;

[ApiController]
[Route("api/v1/admin/eventos")]
[Authorize(Policy = "eventos.criar")]
public class EventosAdminController(CriarEventoUseCase criar, ICurrentUserService currentUser) : ControllerBase
{
    private readonly CriarEventoUseCase _criar = criar;
    private readonly ICurrentUserService _currentUser = currentUser;

    [HttpPost]
    [ProducesResponseType(typeof(Envelope<EventoDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Envelope<EventoDto>>> Criar([FromBody] CriarEventoRequest req, CancellationToken ct)
    {
        if (!_currentUser.TryGetSubject(User, out var organizadorId)) return Unauthorized();
        var dto = await _criar.ExecuteAsync(req, organizadorId, ct);
        return CreatedAtAction(
            actionName: nameof(EventosPublicosController.ObterPorId),
            controllerName: "EventosPublicos",
            routeValues: new { id = dto.Id },
            value: Envelope<EventoDto>.Ok(dto)
        );
    }
}
