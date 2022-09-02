using FluentResults;
using MediatR;

namespace IMDb.Application.Features.Listing.GetActiveClients;
public record GetActiveClientsQuery() : IRequest<Result<IEnumerable<GetActiveClientsQueryResponse>>>;