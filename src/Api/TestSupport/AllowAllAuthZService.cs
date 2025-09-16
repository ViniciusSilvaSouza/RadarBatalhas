using System.Security.Claims;
using Radar.Domain.Compartilhado.Contracts;

namespace Api.TestSupport;

public class AllowAllAuthZService : IServicoAutorizacao
{
    public Task DemandAsync(ClaimsPrincipal user, string permissao) => Task.CompletedTask;
}
