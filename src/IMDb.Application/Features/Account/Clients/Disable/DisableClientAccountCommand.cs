using FluentResults;
using IMDb.Application.Commun;
using MediatR;
using System.Text.Json.Serialization;

namespace IMDb.Application.Features.Account.Clients.Disable;
public record DisableClientAccountCommand : IRequest<Result>
{
    [FromUserInfo]
    [JsonIgnore]
    public Guid Id { get; set; }
    public DisableClientAccountCommand(Guid id)
    {
        Id = id;
    }
}