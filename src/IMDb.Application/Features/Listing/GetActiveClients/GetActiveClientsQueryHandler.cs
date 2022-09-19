using FluentResults;
using IMDb.Domain.Commun;
using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using MediatR;

namespace IMDb.Application.Features.Listing.GetActiveClients;
public class GetActiveClientsQueryHandler : IRequestHandler<GetActiveClientsQuery, Result<IEnumerable<GetActiveClientsQueryResponse>>>
{
    private readonly IUserRepository<Client> userRepository;

    public GetActiveClientsQueryHandler(IUserRepository<Client> userRepository)
    {
        this.userRepository = userRepository;
    }
    public Task<Result<IEnumerable<GetActiveClientsQueryResponse>>> Handle(GetActiveClientsQuery request, CancellationToken cancellationToken)
    {
        var paginated = new PaginatedQueryOptions(request.Page, request.QuatityOfItems, request.IsDescending);
        var clientList = userRepository.GetAllActive(paginated);

        var response = clientList.Select(cl => new GetActiveClientsQueryResponse(cl.Id, cl.Email, cl.Name));

        return Task.FromResult(Result.Ok(response));
    }
}