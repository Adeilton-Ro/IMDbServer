using FluentValidation;

namespace IMDb.Application.Features.Account.Adms.Edit;
public class EditAdmCommandValidation : AbstractValidator<EditAdmCommand>
{
    public EditAdmCommandValidation()
    {
        RuleFor(ec => ec.Name).NotEmpty();
        RuleFor(ec => ec.Email).EmailAddress();
        RuleFor(ec => ec.Password).MinimumLength(8);
    }
}