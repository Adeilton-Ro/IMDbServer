using FluentResults;
using IMDb.Application.Extension;
using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using MediatR;

namespace IMDb.Application.Features.Films.Rate;
public class RateCommandHandler : IRequestHandler<RateCommand, Result<RateCommandResponse>>
{
    private readonly IUserRepository<Client> clientRepository;
    private readonly IFilmRepository filmRepository;
    private readonly IVoteRepository voteRepository;
    private readonly IUnitOfWork unitOfWork;

    public RateCommandHandler(IUserRepository<Client> clientRepository, IFilmRepository filmRepository, 
        IVoteRepository voteRepository, IUnitOfWork unitOfWork)
    {
        this.clientRepository = clientRepository;
        this.filmRepository = filmRepository;
        this.voteRepository = voteRepository;
        this.unitOfWork = unitOfWork;
    }
    public async Task<Result<RateCommandResponse>> Handle(RateCommand request, CancellationToken cancellationToken)
    {
        var client = await clientRepository.GetById(request.Id, cancellationToken);
        if (client is null)
            return Result.Fail(new ApplicationError("The client doesn't exist"));

        var film = await filmRepository.GetById(request.FilmId, cancellationToken);
        if (film is null)
            return Result.Fail(new ApplicationError("The film doesn't exist"));

        if (await voteRepository.IsAlredyRated(request.Id, request.FilmId, cancellationToken))
            return Result.Fail(new ApplicationError("You alredy rate this film"));

        var vote = new Vote
        {
            Id = Guid.NewGuid(),
            ClientId = request.Id,
            FilmId = request.FilmId
        };
        
        var newAvarage = (film.Average * film.Voters + request.Grade) / (film.Voters + 1);

        film.Average = newAvarage;
        film.Voters++;

        filmRepository.Update(film);
        await voteRepository.Create(vote, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(new RateCommandResponse(newAvarage, film.Voters));
    }
}