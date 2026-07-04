namespace CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator.ValueObjects;

public record FillPrice(decimal Price);

public record FillQuantity(decimal Quantity);

public record ExecutionLatencyValue(TimeSpan Latency);

public record ExecutionCost(decimal TotalCost, decimal FeeCost, decimal SlippageCost);

public record ExecutionProbability(decimal Probability);

public record FeeAmount(decimal Amount, string Currency);

public record SlippageAmount(decimal Amount, decimal Bps);

public record ImpactEstimate(decimal Bps, decimal Amount);

public record SimulationSeed(long Seed);
