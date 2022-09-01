using FluentValidation;
using IMDb.Application.Features.ClientSignUp;

public class SignUpCommandValidation : AbstractValidator<SignUpCommand>
{
	public SignUpCommandValidation()
	{
		RuleFor(su => su.Name).NotEmpty();
		RuleFor(su => su.Email).EmailAddress();
		RuleFor(su => su.Password).MinimumLength(8);
	}
}