using MediatR;
using CryptoAIPlatform.Domain.Execution;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Execution;

public class CreateExecutionEngineCommandHandler : IRequestHandler<CreateExecutionEngineCommand, CreateExecutionEngineResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateExecutionEngineCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreateExecutionEngineResponse> Handle(CreateExecutionEngineCommand request, CancellationToken cancellationToken)
    {
        var executionEngine = new ExecutionEngine
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            ExchangeIntegrationId = request.ExchangeIntegrationId,
            IsActive = true
        };

        await _dbContext.ExecutionEngines.AddAsync(executionEngine, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateExecutionEngineResponse
        {
            ExecutionEngineId = executionEngine.Id,
            IsActive = executionEngine.IsActive
        };
    }
}