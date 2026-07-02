using Xunit;
using FluentAssertions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.DataQuality;
using CryptoAIPlatform.Domain.QuantFoundation.Enums;

namespace CryptoAIPlatform.UnitTests.QuantFoundation.DataQuality;

public class DataQualityJobTests
{
    [Fact]
    public void DataQualityJob_Create_ShouldSetAllProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var tenantId = TenantId.New();
        var assetSymbol = "BTCUSDT";
        var periodStart = DateTime.UtcNow.AddDays(-1);
        var periodEnd = DateTime.UtcNow;
        var createdBy = Guid.NewGuid();

        // Act
        var job = DataQualityJob.Create(id, tenantId, assetSymbol, periodStart, periodEnd, createdBy);

        // Assert
        job.Id.Should().Be(id);
        job.TenantId.Should().Be(tenantId);
        job.AssetSymbol.Should().Be(assetSymbol);
        job.PeriodStart.Should().Be(periodStart);
        job.PeriodEnd.Should().Be(periodEnd);
        job.Status.Should().Be(DataQualityJobStatus.Pending);
        job.TotalChecksCount.Should().Be(0);
        job.AnomaliesCount.Should().Be(0);
        job.CreatedBy.Should().Be(createdBy);
    }

    [Fact]
    public void DataQualityJob_Start_ShouldSetStatusToRunning()
    {
        // Arrange
        var job = DataQualityJob.Create(Guid.NewGuid(), TenantId.New(), "BTCUSDT", DateTime.UtcNow.AddDays(-1), DateTime.UtcNow);

        // Act
        job.Start();

        // Assert
        job.Status.Should().Be(DataQualityJobStatus.Running);
    }

    [Fact]
    public void DataQualityJob_Complete_ShouldSetStatusToCompletedAndUpdateCounts()
    {
        // Arrange
        var job = DataQualityJob.Create(Guid.NewGuid(), TenantId.New(), "BTCUSDT", DateTime.UtcNow.AddDays(-1), DateTime.UtcNow);
        var totalChecks = 100;
        var anomaliesCount = 5;

        // Act
        job.Complete(totalChecks, anomaliesCount);

        // Assert
        job.Status.Should().Be(DataQualityJobStatus.Completed);
        job.TotalChecksCount.Should().Be(totalChecks);
        job.AnomaliesCount.Should().Be(anomaliesCount);
    }

    [Fact]
    public void DataQualityJob_Fail_ShouldSetStatusToFailed()
    {
        // Arrange
        var job = DataQualityJob.Create(Guid.NewGuid(), TenantId.New(), "BTCUSDT", DateTime.UtcNow.AddDays(-1), DateTime.UtcNow);

        // Act
        job.Fail();

        // Assert
        job.Status.Should().Be(DataQualityJobStatus.Failed);
    }
}