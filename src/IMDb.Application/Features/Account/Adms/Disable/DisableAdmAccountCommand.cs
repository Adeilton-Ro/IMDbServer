using FluentResults;
using IMDb.Application.Commun;
using MediatR;

namespace IMDb.Application.Features.Account.Adms.Disable;
public record DisableAdmAccountCommand : IRequest<Result>
{
    [FromUserInfo]
    public Guid Id { get; set; }

    public DisableAdmAccountCommand() { }
}