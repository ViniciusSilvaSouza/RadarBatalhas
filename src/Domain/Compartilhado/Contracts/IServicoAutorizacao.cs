using System.Security.Claims;
using System.Threading.Tasks;

namespace Radar.Domain.Compartilhado.Contracts;

public interface IServicoAutorizacao
{
    Task DemandAsync(ClaimsPrincipal user, string permissao);
}
