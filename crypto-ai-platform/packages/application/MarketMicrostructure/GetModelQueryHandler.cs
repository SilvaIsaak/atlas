using MediatR;
using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure;
using CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure.Repositories;

namespace CryptoAIPlatform.Application.MarketMicrostructure;

public class GetModelQueryHandler : IRequestHandler<GetModelQuery, MarketMicrostructureModelDto?>
{
    private readonly IMarketMicrostructureModelRepository _repository;
    private readonly ILogger<GetModelQueryHandler> _logger;

    public GetModelQueryHandler(
        IMarketMicrostructureModelRepository repository,
        ILogger<GetModelQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<MarketMicrostructureModelDto?> Handle(GetModelQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving Market Microstructure model: {Id}", request.ModelId);
        var model = await _repository.GetByIdAsync(request.ModelId, cancellationToken);
        if (model is null) return null;
        return new MarketMicrostructureModelDto(
            Id: model.Id,
            Name: model.Name,
            AssetSymbol: model.AssetSymbol,
            CalibratedAt: model.CalibratedAt,
            IsActive: model.IsActive);
    }
}
