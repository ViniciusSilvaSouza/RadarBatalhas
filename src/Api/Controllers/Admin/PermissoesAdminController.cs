using Application.DTOs.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers.Admin;

[ApiController]
[Route("api/v1/admin/permissoes")]
[Authorize(Policy = "admin.somente")]
public class PermissoesAdminController(Infrastructure.Persistence.RadarDbContext db) : ControllerBase
{
    private readonly Infrastructure.Persistence.RadarDbContext _db = db;

    [HttpGet]
    public async Task<ActionResult<Envelope<IEnumerable<object>>>> Get(CancellationToken ct)
    {
        var dados = await _db.Permissoes
            .Select(p => new { p.Id, p.Codigo })
            .ToListAsync(ct);
        return Ok(Envelope<IEnumerable<object>>.Ok(dados));
    }
}
