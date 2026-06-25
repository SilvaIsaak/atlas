using MediatR;
using CryptoAIPlatform.Domain.Indicators;

namespace CryptoAIPlatform.Application.Indicators;

public record CalculateSmaQuery(List<decimal> Prices, int Period) : IRequest<SmaResult>;

public class CalculateSmaQueryHandler : IRequestHandler<CalculateSmaQuery, SmaResult>
{
    public async Task<SmaResult> Handle(CalculateSmaQuery request, CancellationToken cancellationToken)
    {
        var indicator = new SmaIndicator();
        return await Task.FromResult(indicator.Calculate(new SmaInput(request.Prices, request.Period)));
    }
}
