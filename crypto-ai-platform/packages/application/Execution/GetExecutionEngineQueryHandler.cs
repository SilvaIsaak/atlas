using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Execution;

public class GetExecutionEngineQueryHandler : IRequestHandler<GetExecutionEngineQuery, GetExecutionEngineResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public GetExecutionEngineQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetExecutionEngineResponse> Handle(GetExecutionEngineQuery request, CancellationToken cancellationToken)
    {
        var executionEngine = await _dbContext.ExecutionEngines
            .FirstOrDefaultAsync(ee => ee.Id == request.ExecutionEngineId && ee.UserId == request.UserId, cancellationToken);

        if (executionEngine == null)
        {
            throw new KeyNotFoundException("ExecutionEngine not found");
        }

        return new GetExecutionEngineResponse(
            executionEngine.Id,
            executionEngine.UserId,
            executionEngine.ExchangeIntegrationId,
            executionEngine.Orders,
            executionEngine.IsActive,
            executionEngine.CreatedAt,
            executionEngine.UpdatedAt
        );
    }
}