using FluentValidation;

namespace IMDb.Application.Features.Account.Clients.Edit;
public class EditClientCommandValidation : AbstractValidator<EditClientCommand>
{
    public EditClientCommandValidation()
    {
        RuleFor(ec => ec.Name).NotEmpty();
        RuleFor(ec => ec.Email).EmailAddress();
        RuleFor(ec => ec.Password).MinimumLength(8);
    }
}
