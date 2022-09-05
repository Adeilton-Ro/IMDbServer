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
    public async Task<Result<IEnumerable<GetActiveClientsQueryResponse>>> Handle(GetActiveClientsQuery request, CancellationToken cancellationToken)
    {
        var clientList = new List<Client>();

        if (request.IsDescending)
            clientList = userRepository.GetAllActiveDescending();
        else
            clientList = userRepository.GetAllActive();

        var response = clientList.Select(cl => new GetActiveClientsQueryResponse(cl.Id, cl.Email, cl.Name));

        if (request.QuatityOfItems != 0)
        {
            var quantityDb = clientList.Count - 1;

            var start = request.QuatityOfItems * (request.Page - 1);
            start = clientList.Count < start ? quantityDb : start;

            var end = start + request.QuatityOfItems;
            end = clientList.Count < end? quantityDb: end;

            var pagedClient = clientList.Skip(start).Take(end);
            response = pagedClient.Select(cl => new GetActiveClientsQueryResponse(cl.Id, cl.Email, cl.Name));
            return Result.Ok(response);
        }

        return Result.Ok(response);
    }
}