using FluentValidation;

namespace IMDb.Application.Features.Login;
public class LoginCommandValidation : AbstractValidator<LoginCommand>
{
	public LoginCommandValidation()
	{
		RuleFor(lc => lc.Email).EmailAddress();
		RuleFor(lc => lc.Password).MinimumLength(8);
	}
}
