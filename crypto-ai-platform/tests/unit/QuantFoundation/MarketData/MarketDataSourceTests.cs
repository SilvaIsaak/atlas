using Xunit;
using FluentAssertions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData;
using CryptoAIPlatform.Domain.QuantFoundation.Enums;

namespace CryptoAIPlatform.UnitTests.QuantFoundation.MarketData;

public class MarketDataSourceTests
{
    [Fact]
    public void MarketDataSource_Create_ShouldSetAllProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var tenantId = TenantId.New();
        var name = "Binance Spot";
        var baseUrl = "https://api.binance.com";
        var type = MarketDataSourceType.Binance;
        var isActive = true;
        var encryptedApiKey = "encrypted-key";
        var apiKeyNonce = "nonce";
        var apiKeyTag = "tag";
        var createdBy = Guid.NewGuid();

        // Act
        var dataSource = MarketDataSource.Create(id, tenantId, name, baseUrl, type, isActive, encryptedApiKey, apiKeyNonce, apiKeyTag, createdBy);

        // Assert
        dataSource.Id.Should().Be(id);
        dataSource.TenantId.Should().Be(tenantId);
        dataSource.Name.Should().Be(name);
        dataSource.BaseUrl.Should().Be(baseUrl);
        dataSource.Type.Should().Be(type);
        dataSource.IsActive.Should().Be(isActive);
        dataSource.EncryptedApiKey.Should().Be(encryptedApiKey);
        dataSource.ApiKeyNonce.Should().Be(apiKeyNonce);
        dataSource.ApiKeyTag.Should().Be(apiKeyTag);
        dataSource.CreatedBy.Should().Be(createdBy);
        dataSource.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void MarketDataSource_Update_ShouldUpdateProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var tenantId = TenantId.New();
        var dataSource = MarketDataSource.Create(id, tenantId, "Old Name", "https://old.url", MarketDataSourceType.Binance);
        var newName = "New Name";
        var newBaseUrl = "https://new.url";
        var newIsActive = false;
        var newEncryptedApiKey = "new-encrypted-key";
        var newApiKeyNonce = "new-nonce";
        var newApiKeyTag = "new-tag";
        var updatedBy = Guid.NewGuid();

        // Act
        dataSource.Update(newName, newBaseUrl, newIsActive, newEncryptedApiKey, newApiKeyNonce, newApiKeyTag, updatedBy);

        // Assert
        dataSource.Name.Should().Be(newName);
        dataSource.BaseUrl.Should().Be(newBaseUrl);
        dataSource.IsActive.Should().Be(newIsActive);
        dataSource.EncryptedApiKey.Should().Be(newEncryptedApiKey);
        dataSource.ApiKeyNonce.Should().Be(newApiKeyNonce);
        dataSource.ApiKeyTag.Should().Be(newApiKeyTag);
        dataSource.UpdatedBy.Should().Be(updatedBy);
        dataSource.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void MarketDataSource_AddIngestionJob_ShouldAddJobToList()
    {
        // Arrange
        var dataSourceId = Guid.NewGuid();
        var tenantId = TenantId.New();
        var dataSource = MarketDataSource.Create(dataSourceId, tenantId, "Test", "https://test.url", MarketDataSourceType.Binance);
        var jobId = Guid.NewGuid();
        var job = MarketDataIngestionJob.Create(jobId, dataSourceId, "BTCUSDT", MarketDataType.Ohlcv);

        // Act
        dataSource.AddIngestionJob(job);

        // Assert
        dataSource.IngestionJobs.Should().Contain(job);
        dataSource.IngestionJobs.Count.Should().Be(1);
    }
}