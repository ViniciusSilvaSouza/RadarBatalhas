using Application.DTOs.Common;
using Application.DTOs.Ranking;
using AutoMapper;
using Domain.Repositorios.Ranking;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Public;

[ApiController]
[Route("api/v1/public/ranking")]
public class RankingController(IRankingLeituraRepositorio repo, IMapper mapper) : ControllerBase
{
    private readonly IRankingLeituraRepositorio _repo = repo;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult<Envelope<IEnumerable<ItemRankingDto>>>> Get(CancellationToken ct)
    {
        var items = await _repo.ObterRankingAsync(ct);
        var dto = _mapper.Map<IEnumerable<ItemRankingDto>>(items);
        return Ok(Envelope<IEnumerable<ItemRankingDto>>.Ok(dto));
    }
}
