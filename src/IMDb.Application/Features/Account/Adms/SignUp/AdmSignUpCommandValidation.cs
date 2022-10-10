using FluentValidation;

namespace IMDb.Application.Features.Account.Adms.SignUp;
public class AdmSignUpCommandValidation : AbstractValidator<AdmSignUpCommand>
{
    public AdmSignUpCommandValidation()
    {
        RuleFor(asc => asc.Email).EmailAddress();
        RuleFor(asc => asc.Password).MinimumLength(8);
        RuleFor(asc => asc.Name).NotEmpty();
    }
}
