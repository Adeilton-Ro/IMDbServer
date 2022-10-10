using FluentResults;
using IMDb.Application.Commun;
using MediatR;

namespace IMDb.Application.Features.Account.Adms.Edit;
public record EditAdmCommand : IRequest<Result>
{
    [FromUserInfo]
    public Guid Id { get; set; }
    public string Name { get; }
    public string Email { get; }

    public EditAdmCommand() { }
    public EditAdmCommand(string name, string email)
    {
        Name = name;
        Email = email;
    }
}