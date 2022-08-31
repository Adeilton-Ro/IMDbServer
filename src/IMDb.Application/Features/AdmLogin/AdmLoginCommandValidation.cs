using FluentValidation;

namespace IMDb.Application.Features.AdmLogin;
public class AdmLoginCommandValidation : AbstractValidator<AdmLoginCommand>
{
	public AdmLoginCommandValidation()
	{
        RuleFor(alc => alc.Email).EmailAddress();
        RuleFor(alc => alc.Password).MinimumLength(8);
    }
}