using FluentResults;
using IMDb.Application.Commun;
using MediatR;
using System.Text.Json.Serialization;

namespace IMDb.Application.Features.Account.Adms.Disable;
public record DisableAdmAccountCommand : IRequest<Result>
{
    [FromUserInfo]
    [JsonIgnore]
    public Guid Id { get; set; }

    public DisableAdmAccountCommand(Guid id)
    {
        this.Id = id;
    }
}