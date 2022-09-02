using FluentResults;
using IMDb.Application.Commun;
using MediatR;
using System.Text.Json.Serialization;

namespace IMDb.Application.Features.Account.Adms.Edit;
public record EditAdmCommand : IRequest<Result>
{
    [FromUserInfo]
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Name { get; }
    public string Email { get; }
    public string Password { get; }

    public EditAdmCommand(Guid id, string name, string email, string password)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
    }
}