using Application.Contracts;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class S3FileStorageService : IFileStorageService
{
    private readonly IAmazonS3 _s3;
    private readonly string _bucket;
    private readonly string _baseUrl;

    public S3FileStorageService(IAmazonS3 s3, IConfiguration cfg)
    {
        _s3 = s3;
        _bucket = cfg["S3_BUCKET_NAME"] ?? throw new InvalidOperationException("S3_BUCKET_NAME not configured");
        _baseUrl = cfg["S3_PUBLIC_BASE_URL"] ?? $"https://{_bucket}.s3.amazonaws.com";
    }

    public async Task<string> SaveEventImageAsync(Guid eventoId, Stream content, string fileName, string contentType, CancellationToken ct = default)
    {
        var ext = Path.GetExtension(fileName);
        var key = $"eventos/{eventoId}/{Guid.NewGuid():N}{ext}";
        using var transfer = new TransferUtility(_s3);
        var req = new TransferUtilityUploadRequest
        {
            InputStream = content,
            BucketName = _bucket,
            Key = key,
            ContentType = contentType,
            CannedACL = S3CannedACL.PublicRead
        };
        await transfer.UploadAsync(req, ct);
        return $"{_baseUrl}/{key}";
    }
}
