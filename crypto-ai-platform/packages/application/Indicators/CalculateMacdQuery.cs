using MediatR;
using CryptoAIPlatform.Domain.Indicators;

namespace CryptoAIPlatform.Application.Indicators;

public record CalculateMacdQuery(List<decimal> Prices, int FastPeriod = 12, int SlowPeriod = 26, int SignalPeriod = 9) : IRequest<MacdResult>;

public class CalculateMacdQueryHandler : IRequestHandler<CalculateMacdQuery, MacdResult>
{
    public async Task<MacdResult> Handle(CalculateMacdQuery request, CancellationToken cancellationToken)
    {
        var indicator = new MacdIndicator();
        return await Task.FromResult(indicator.Calculate(new MacdInput(request.Prices, request.FastPeriod, request.SlowPeriod, request.SignalPeriod)));
    }
}
