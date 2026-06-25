using MediatR;
using CryptoAIPlatform.Domain.Indicators;

namespace CryptoAIPlatform.Application.Indicators;

public record CalculateBollingerBandsQuery(List<decimal> Prices, int Period = 20, decimal StdDevMultiplier = 2) : IRequest<BollingerBandsResult>;

public class CalculateBollingerBandsQueryHandler : IRequestHandler<CalculateBollingerBandsQuery, BollingerBandsResult>
{
    public async Task<BollingerBandsResult> Handle(CalculateBollingerBandsQuery request, CancellationToken cancellationToken)
    {
        var indicator = new BollingerBandsIndicator();
        return await Task.FromResult(indicator.Calculate(new BollingerBandsInput(request.Prices, request.Period, request.StdDevMultiplier)));
    }
}
