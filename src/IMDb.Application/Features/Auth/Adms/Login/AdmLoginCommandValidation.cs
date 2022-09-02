using FluentValidation;

namespace IMDb.Application.Features.Auth.Adms.Login;
public class AdmLoginCommandValidation : AbstractValidator<AdmLoginCommand>
{
    public AdmLoginCommandValidation()
    {
        RuleFor(alc => alc.Email).EmailAddress();
        RuleFor(alc => alc.Password).MinimumLength(8);
    }
}