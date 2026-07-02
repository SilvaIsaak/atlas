using Xunit;
using FluentAssertions;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData.ValueObjects;

namespace CryptoAIPlatform.UnitTests.QuantFoundation.MarketData.ValueObjects;

public class OhlcvDataTests
{
    [Fact]
    public void OhlcvData_Create_ShouldSetAllProperties()
    {
        // Arrange
        var timestamp = DateTime.UtcNow;
        var open = 100.5m;
        var high = 110.2m;
        var low = 95.3m;
        var close = 105.8m;
        var volume = 1500.75m;

        // Act
        var ohlcv = new OhlcvData(timestamp, open, high, low, close, volume);

        // Assert
        ohlcv.Timestamp.Should().Be(timestamp);
        ohlcv.Open.Should().Be(open);
        ohlcv.High.Should().Be(high);
        ohlcv.Low.Should().Be(low);
        ohlcv.Close.Should().Be(close);
        ohlcv.Volume.Should().Be(volume);
    }

    [Fact]
    public void OhlcvData_WithSameValues_ShouldBeEqual()
    {
        // Arrange
        var timestamp = DateTime.UtcNow;
        var ohlcv1 = new OhlcvData(timestamp, 100, 110, 95, 105, 1000);
        var ohlcv2 = new OhlcvData(timestamp, 100, 110, 95, 105, 1000);

        // Act & Assert
        ohlcv1.Should().Be(ohlcv2);
        (ohlcv1 == ohlcv2).Should().BeTrue();
    }

    [Fact]
    public void OhlcvData_WithDifferentValues_ShouldNotBeEqual()
    {
        // Arrange
        var timestamp = DateTime.UtcNow;
        var ohlcv1 = new OhlcvData(timestamp, 100, 110, 95, 105, 1000);
        var ohlcv2 = new OhlcvData(timestamp, 101, 110, 95, 105, 1000);

        // Act & Assert
        ohlcv1.Should().NotBe(ohlcv2);
        (ohlcv1 != ohlcv2).Should().BeTrue();
    }
}