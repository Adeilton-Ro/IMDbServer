using FluentValidation;
using IMDb.Application.Features.Account.Clients.SignUp;

public class SignUpCommandValidation : AbstractValidator<SignUpCommand>
{
	public SignUpCommandValidation()
	{
		RuleFor(su => su.Name).NotEmpty();
		RuleFor(su => su.Email).EmailAddress();
		RuleFor(su => su.Password).MinimumLength(8);
	}
}