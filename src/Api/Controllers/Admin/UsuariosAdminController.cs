using Application.DTOs.Common;
using Application.DTOs.Usuarios;
using Application.UseCases.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Admin;

[ApiController]
[Route("api/v1/admin/usuarios")] 
[Authorize(Policy = "admin.somente")]
public class UsuariosAdminController(CriarOuAtualizarUsuarioAdminUseCase criarOuAtualizar) : ControllerBase
{
    private readonly CriarOuAtualizarUsuarioAdminUseCase _useCase = criarOuAtualizar;

    [HttpPost]
    public async Task<ActionResult<Envelope<object>>> Criar([FromBody] CriarUsuarioRequest req, CancellationToken ct)
    {
        try
        {
            var env = await _useCase.ExecuteAsync(req, ct);
            return Ok(env);
        }
        catch (InvalidOperationException ex)
        {
            var corr = HttpContext.Response.Headers["X-Correlation-Id"].ToString();
            return BadRequest(new Application.DTOs.Common.ErrorDetails(400, "PapelInvalido", ex.Message, corr));
        }
    }
}
