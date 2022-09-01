using FluentResults;
using IMDb.Application.Commun;
using MediatR;
using System.Text.Json.Serialization;

namespace IMDb.Application.Features.ClientEdit;
public record EditClientCommand(Guid Id, string Name, string Email, string Password) : IRequest<Result>
{
    [FromUserInfo]
    [JsonIgnore]
    public Guid Id { get; set; }
}