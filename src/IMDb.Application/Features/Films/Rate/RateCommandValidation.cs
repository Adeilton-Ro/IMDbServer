using FluentValidation;

namespace IMDb.Application.Features.Films.Rate;
public class RateCommandValidation : AbstractValidator<RateCommand>
{
	public RateCommandValidation()
	{
		RuleFor(rc => rc.Grade).GreaterThanOrEqualTo(0).LessThanOrEqualTo(4);
	}
}