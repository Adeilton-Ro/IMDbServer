using FluentResults;
using IMDb.Application.Commun;
using MediatR;

namespace IMDb.Application.Features.Account.Clients.Edit;
public record EditClientCommand : IRequest<Result>
{
    [FromUserInfo]
    public Guid Id { get; set; }
    public string Name { get; }
    public string Email { get; }

    public EditClientCommand() { }
    public EditClientCommand(string name, string email)
    {
        Name = name;
        Email = email;
    }
}