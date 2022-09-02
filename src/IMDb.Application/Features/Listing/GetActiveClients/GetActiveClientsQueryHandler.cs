using FluentResults;
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
        var response = userRepository.GetAllActive().Select(c => new GetActiveClientsQueryResponse(c.Id,c.Email, c.Name));
        return Task.FromResult(Result.Ok(response));
    }
}