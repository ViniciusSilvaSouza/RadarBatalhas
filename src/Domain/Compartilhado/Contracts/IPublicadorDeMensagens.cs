using System.Threading;
using System.Threading.Tasks;

namespace Radar.Domain.Compartilhado.Contracts;

public interface IPublicadorDeMensagens
{
    Task PublicarAsync<T>(string topic, T payload, CancellationToken ct = default);
}
