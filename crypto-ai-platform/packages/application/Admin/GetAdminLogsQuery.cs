using MediatR;

namespace CryptoAIPlatform.Application.Admin;

public record GetAdminLogsQuery : IRequest<List<GetAdminLogResponse>>
{
    public int Limit { get; init; } = 100;
}

public record GetAdminLogResponse(
    Guid LogId,
    Guid AdminUserId,
    string Action,
    string? TargetEntity,
    string? TargetId,
    string? Details,
    DateTime CreatedAt
);
