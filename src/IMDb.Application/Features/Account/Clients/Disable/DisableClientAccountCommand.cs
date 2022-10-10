using FluentResults;
using IMDb.Application.Commun;
using MediatR;

namespace IMDb.Application.Features.Account.Clients.Disable;
public record DisableClientAccountCommand : IRequest<Result>
{
    [FromUserInfo]
    public Guid Id { get; set; }
    public DisableClientAccountCommand() { }
}