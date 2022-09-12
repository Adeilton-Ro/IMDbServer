using FluentValidation;

namespace IMDb.Application.Features.Films.NewFilm;
public class NewFilmCommandValidation : AbstractValidator<NewFilmCommand>
{
    public NewFilmCommandValidation()
    {
        RuleFor(nfc => nfc.Name).NotEmpty();
        RuleFor(nfc => nfc.Directors).NotEmpty();
        RuleFor(nfc => nfc.Actors).NotEmpty();
        RuleFor(nfc => nfc.Genders).NotEmpty();
    }
}