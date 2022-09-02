using FluentResults;
using IMDb.Application.Extension;
using IMDb.Application.Services.Crypto;
using IMDb.Domain.Entities;
using IMDb.Domain.Entities.Abstract;
using IMDb.Infra.Database.Abstraction.Interfaces;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using MediatR;

namespace IMDb.Application.Features.Account.Clients.SignUp;
public class SignUpCommandHandler : IRequestHandler<SignUpCommand, Result<SignUpCommandResponse>>
{
    private readonly IUserRepository<Client> userRepository;
    private readonly ICryptographyService cryptographyService;
    private readonly IUnitOfWork unitOfWork;

    public SignUpCommandHandler(IUserRepository<Client> userRepository, ICryptographyService cryptographyService, IUnitOfWork unitOfWork)
    {
        this.userRepository = userRepository;
        this.cryptographyService = cryptographyService;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<SignUpCommandResponse>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        if (!await userRepository.IsUniqueEmail(request.Email.ToLower(), cancellationToken))
            return Result.Fail(new ApplicationError("This email is alredy in use"));

        var salt = cryptographyService.CreateSalt();
        var user = new Client
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email.ToLower(),
            Salt = salt,
            Hash = cryptographyService.Hash(request.Password, salt),
            isActive = true
        };

        await userRepository.Create(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(new SignUpCommandResponse(user.Id));
    }
}