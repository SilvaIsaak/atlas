namespace CryptoAIPlatform.Infrastructure.Options;

public class StorageOptions
{
    public const string SectionName = "Storage";
    public string Provider { get; set; } = "AzureBlob"; // AzureBlob or AwsS3
    public AzureBlobOptions? AzureBlob { get; set; }
    public AwsS3Options? AwsS3 { get; set; }
}

public class AzureBlobOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public string ContainerName { get; set; } = "cryptoaiplatform";
}

public class AwsS3Options
{
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string BucketName { get; set; } = "cryptoaiplatform";
    public string Region { get; set; } = "us-east-1";
}
