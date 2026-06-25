using MediatR;
using CryptoAIPlatform.Domain.Indicators;

namespace CryptoAIPlatform.Application.Indicators;

public record CalculateRsiQuery(List<decimal> Prices, int Period = 14) : IRequest<RsiResult>;

public class CalculateRsiQueryHandler : IRequestHandler<CalculateRsiQuery, RsiResult>
{
    public async Task<RsiResult> Handle(CalculateRsiQuery request, CancellationToken cancellationToken)
    {
        var indicator = new RsiIndicator();
        return await Task.FromResult(indicator.Calculate(new RsiInput(request.Prices, request.Period)));
    }
}
