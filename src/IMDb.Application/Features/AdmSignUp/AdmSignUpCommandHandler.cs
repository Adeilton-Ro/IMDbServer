using FluentResults;
using IMDb.Application.Extension;
using IMDb.Application.Services.Crypto;
using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using MediatR;

namespace IMDb.Application.Features.AdmSignUp;
public class AdmSignUpCommandHandler : IRequestHandler<AdmSignUpCommand, Result<AdmSignUpCommandResponse>>
{
    private readonly IUserRepository<Adm> userRepository;
    private readonly ICryptographyService cryptographyService;
    private readonly IUnitOfWork unitOfWork;

    public AdmSignUpCommandHandler(IUserRepository<Adm> userRepository, ICryptographyService cryptographyService, IUnitOfWork unitOfWork)
    {
        this.userRepository = userRepository;
        this.cryptographyService = cryptographyService;
        this.unitOfWork = unitOfWork;
    }
    public async Task<Result<AdmSignUpCommandResponse>> Handle(AdmSignUpCommand request, CancellationToken cancellationToken)
    {
        if (!await userRepository.IsUniqueEmail(request.Email.ToLower(), cancellationToken))
            return Result.Fail(new ApplicationError("This email is alredy in use"));

        var salt = cryptographyService.CreateSalt();
        var user = new Adm
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email.ToLower(),
            Salt = salt,
            Hash = cryptographyService.Hash(request.Password, salt)
        };

        await userRepository.Create(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(new AdmSignUpCommandResponse(user.Id));
    }
}
