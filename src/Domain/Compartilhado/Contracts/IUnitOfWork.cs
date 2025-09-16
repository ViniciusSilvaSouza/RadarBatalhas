using System.Threading;
using System.Threading.Tasks;

namespace Domain.Compartilhado.Contracts;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
