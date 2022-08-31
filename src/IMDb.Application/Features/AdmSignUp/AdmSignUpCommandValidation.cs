using FluentValidation;

namespace IMDb.Application.Features.AdmSignUp;
public class AdmSignUpCommandValidation : AbstractValidator<AdmSignUpCommand>
{
	public AdmSignUpCommandValidation()
	{
		RuleFor(asc => asc.Email).EmailAddress();
		RuleFor(asc => asc.Password).MinimumLength(8);
		RuleFor(asc => asc.Name).NotEmpty();
	}
}
