using Application.Contracts;

namespace Infrastructure.Services;

public class LocalFileStorageService : IFileStorageService
{
    private readonly string _basePath;
    private readonly string _publicPrefix;

    public LocalFileStorageService()
    {
        var root = AppContext.BaseDirectory;
        _basePath = Path.Combine(root, "wwwroot", "uploads");
        _publicPrefix = "/uploads";
    }

    public async Task<string> SaveEventImageAsync(Guid eventoId, Stream content, string fileName, string contentType, CancellationToken ct = default)
    {
        Directory.CreateDirectory(Path.Combine(_basePath, "eventos", eventoId.ToString()));
        var folder = Path.Combine(_basePath, "eventos", eventoId.ToString());
        var ext = Path.GetExtension(fileName);
        var name = $"{Guid.NewGuid():N}{ext}";
        var fullPath = Path.Combine(folder, name);
        await using var fs = new FileStream(fullPath, FileMode.Create);
        await content.CopyToAsync(fs, ct);
        var publicUrl = $"{_publicPrefix}/eventos/{eventoId}/{name}".Replace("\\", "/");
        return publicUrl;
    }
}
