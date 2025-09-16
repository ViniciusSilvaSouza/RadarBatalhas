using Application.DTOs.Common;
using Application.DTOs.Eventos;
using Application.UseCases.Evento;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Contracts;

namespace Api.Controllers.Admin;

[ApiController]
[Route("api/v1/admin/meus-eventos")] 
[Authorize]
public class EventosOrganizadorController(ListarEventosDoOrganizadorUseCase listar, ICurrentUserService currentUser) : ControllerBase
{
    private readonly ListarEventosDoOrganizadorUseCase _listar = listar;
    private readonly ICurrentUserService _currentUser = currentUser;

    [HttpGet]
    [ProducesResponseType(typeof(Envelope<IEnumerable<EventoDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Envelope<IEnumerable<EventoDto>>>> Get(CancellationToken ct)
    {
        if (!_currentUser.TryGetSubject(User, out var organizadorId)) return Unauthorized();
        var dto = await _listar.ExecuteAsync(organizadorId, ct);
        return Ok(Envelope<IEnumerable<EventoDto>>.Ok(dto));
    }
}
