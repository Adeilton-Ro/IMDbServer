using FluentValidation;

namespace IMDb.Application.Features.Listing.GetActiveClients;
public class GetActiveClientsQueryValidation : AbstractValidator<GetActiveClientsQuery>
{
	public GetActiveClientsQueryValidation()
	{
		RuleFor(gac => gac.QuatityOfItems).GreaterThanOrEqualTo(0);
		RuleFor(gac => gac.Page).GreaterThanOrEqualTo(0);
	}
}