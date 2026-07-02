namespace CryptoAIPlatform.Domain.Core.Abstractions;

public interface IColdStorageService
{
    Task UploadAsync(string path, byte[] data, CancellationToken cancellationToken = default);
    Task<byte[]> DownloadAsync(string path, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string path, CancellationToken cancellationToken = default);
    Task DeleteAsync(string path, CancellationToken cancellationToken = default);
}
