using FluentValidation;

namespace IMDb.Application.Features.ClientLogin;
public class LoginCommandValidation : AbstractValidator<LoginCommand>
{
    public LoginCommandValidation()
    {
        RuleFor(lc => lc.Email).EmailAddress();
        RuleFor(lc => lc.Password).MinimumLength(8);
    }
}
