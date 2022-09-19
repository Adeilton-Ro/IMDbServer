using FluentResults;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using MediatR;

namespace IMDb.Application.Features.Films.GetGender;
public class GetGenderQueryHandler : IRequestHandler<GetGenderQuery, Result<IEnumerable<GetGenderQueryResponse>>>
{
    private readonly IGenderRepository genderRepository;

    public GetGenderQueryHandler(IGenderRepository genderRepository)
    {
        this.genderRepository = genderRepository;
    }
    public Task<Result<IEnumerable<GetGenderQueryResponse>>> Handle(GetGenderQuery request, CancellationToken cancellationToken)
    {
        var response = genderRepository.GetAll().Select(g => new GetGenderQueryResponse(g.Id, g.Name));
        return Task.FromResult(Result.Ok(response));
    }
}