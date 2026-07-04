namespace CryptoAIPlatform.Infrastructure.Options;

public class RedisOptions
{
    public const string SectionName = "Redis";
    public string ConnectionString { get; set; } = "localhost:6379";
    public string InstanceName { get; set; } = "cryptoaiplatform";
    public TimeSpan DefaultTtl { get; set; } = TimeSpan.FromMinutes(30);
    public bool UseCompression { get; set; } = false;
}
