using FluentResults;
using MediatR;

namespace IMDb.Application.Features.Listing.GetActiveClients;
public record GetActiveClientsQuery(int Page, int QuatityOfItems, bool? IsDescending) : IRequest<Result<IEnumerable<GetActiveClientsQueryResponse>>>;