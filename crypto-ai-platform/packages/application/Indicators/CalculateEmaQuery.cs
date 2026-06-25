using MediatR;
using CryptoAIPlatform.Domain.Indicators;

namespace CryptoAIPlatform.Application.Indicators;

public record CalculateEmaQuery(List<decimal> Prices, int Period) : IRequest<EmaResult>;

public class CalculateEmaQueryHandler : IRequestHandler<CalculateEmaQuery, EmaResult>
{
    public async Task<EmaResult> Handle(CalculateEmaQuery request, CancellationToken cancellationToken)
    {
        var indicator = new EmaIndicator();
        return await Task.FromResult(indicator.Calculate(new EmaInput(request.Prices, request.Period)));
    }
}
