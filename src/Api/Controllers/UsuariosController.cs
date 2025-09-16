using Application.DTOs.Common;
using Application.DTOs.Usuarios;
using Application.UseCases.Usuarios;
using Domain.Compartilhado.Contracts;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.Contracts;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/usuarios")] 
public class UsuariosController(SelfOnboardUseCase selfOnboard, IUnitOfWork uow, RadarDbContext db, ICurrentUserService currentUser) : ControllerBase
{
    private readonly SelfOnboardUseCase _selfOnboard = selfOnboard;
    private readonly RadarDbContext _db = db;
    private readonly ICurrentUserService _currentUser = currentUser;

    // Qualquer usuário autenticado pode se auto-registrar; não exige permissão de admin
    [HttpPost("self")]
    [Authorize]
    public async Task<ActionResult<Envelope<object>>> SelfOnboard([FromBody] SelfOnboardRequest req, CancellationToken ct)
    {
        if (!_currentUser.TryGetSubject(User, out var userId))
            return Unauthorized();

        var roles = _currentUser.GetRoles(User);
        var env = await _selfOnboard.ExecuteAsync(userId, req, roles, ct);
        return Ok(env);
    }
}
