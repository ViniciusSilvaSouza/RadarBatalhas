using Api.Controllers.Public;
using Application.DTOs.Common;
using Application.DTOs.Eventos;
using Application.UseCases.Evento;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Contracts;
using Microsoft.AspNetCore.Http;

namespace Api.Controllers.Admin;

[ApiController]
[Route("api/v1/admin/eventos")]
[Authorize(Policy = "eventos.criar")]
public class EventosAdminController(CriarEventoUseCase criar, ICurrentUserService currentUser, IFileStorageService storage, Domain.Repositorios.Eventos.IEventoRepositorio eventoRepo, Domain.Compartilhado.Contracts.IUnitOfWork uow) : ControllerBase
{
    private readonly CriarEventoUseCase _criar = criar;
    private readonly ICurrentUserService _currentUser = currentUser;
    private readonly IFileStorageService _storage = storage;
    private readonly Domain.Repositorios.Eventos.IEventoRepositorio _eventoRepo = eventoRepo;
    private readonly Domain.Compartilhado.Contracts.IUnitOfWork _uow = uow;

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

    [HttpPost("{id:guid}/imagem")]
    [RequestSizeLimit(10_000_000)] // 10 MB
    public async Task<ActionResult<Envelope<object>>> UploadImagem(Guid id, IFormFile arquivo, CancellationToken ct)
    {
        if (arquivo is null || arquivo.Length == 0)
        {
            var corr = HttpContext.Response.Headers["X-Correlation-Id"].ToString();
            return BadRequest(new ErrorDetails(400, "ArquivoInvalido", "Arquivo inválido", corr));
        }
        if (arquivo.ContentType is not ("image/jpeg" or "image/png" or "image/webp"))
        {
            var corr = HttpContext.Response.Headers["X-Correlation-Id"].ToString();
            return BadRequest(new ErrorDetails(400, "FormatoNaoSuportado", "Formato não suportado", corr));
        }

        await using var stream = arquivo.OpenReadStream();
        var url = await _storage.SaveEventImageAsync(id, stream, arquivo.FileName, arquivo.ContentType, ct);

        var e = await _eventoRepo.ObterPorIdAsync(id, ct);
        if (e is null) return NotFound();
        e.ImagemUrl = url;
        await _uow.SaveChangesAsync(ct);

        return Ok(Envelope<object>.Ok(new { ImagemUrl = url }));
    }
}
