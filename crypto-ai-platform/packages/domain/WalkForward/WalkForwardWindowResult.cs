namespace CryptoAIPlatform.Domain.WalkForward;

public record WalkForwardWindowResult(
    int WindowNumber,
    DateTime InSampleStart,
    DateTime InSampleEnd,
    DateTime OutOfSampleStart,
    DateTime OutOfSampleEnd,
    decimal InSampleReturn,
    decimal OutOfSampleReturn,
    decimal SharpeRatio,
    decimal MaxDrawdown
);