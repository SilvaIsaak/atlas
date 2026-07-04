using MediatR;
using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure;
using CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure.Repositories;
using CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure.ValueObjects;

namespace CryptoAIPlatform.Application.MarketMicrostructure;

public class CreateModelCommandHandler : IRequestHandler<CreateModelCommand, MarketMicrostructureModelDto>
{
    private readonly IMarketMicrostructureModelRepository _repository;
    private readonly ILogger<CreateModelCommandHandler> _logger;

    public CreateModelCommandHandler(
        IMarketMicrostructureModelRepository repository,
        ILogger<CreateModelCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<MarketMicrostructureModelDto> Handle(CreateModelCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating Market Microstructure model: {Name} for asset {Asset}", request.Name, request.AssetSymbol);
        var model = MarketMicrostructureModel.Create(
            id: Guid.NewGuid(),
            tenantId: TenantId.Default,
            name: request.Name,
            assetSymbol: request.AssetSymbol);
        await _repository.AddAsync(model, cancellationToken);
        return new MarketMicrostructureModelDto(
            Id: model.Id,
            Name: model.Name,
            AssetSymbol: model.AssetSymbol,
            CalibratedAt: model.CalibratedAt,
            IsActive: model.IsActive);
    }
}
