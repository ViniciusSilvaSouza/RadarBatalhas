using System.IO;

namespace Application.Contracts;

public interface IFileStorageService
{
    Task<string> SaveEventImageAsync(Guid eventoId, Stream content, string fileName, string contentType, CancellationToken ct = default);
}
