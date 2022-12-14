using FluentResults;
using IMDb.Application.Extension;
using IMDb.Application.Services.Crypto;
using IMDb.Application.Services.Token;
using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using MediatR;

namespace IMDb.Application.Features.Auth.Adms.Login;
public class AdmLoginCommandHandler : IRequestHandler<AdmLoginCommand, Result<AdmLoginCommandResponse>>
{
    private readonly IUserRepository<Adm> userRepository;
    private readonly ICryptographyService cryptographyService;
    private readonly ITokenService tokenService;

    public AdmLoginCommandHandler(IUserRepository<Adm> userRepository, ICryptographyService cryptographyService, ITokenService tokenService)
    {
        this.userRepository = userRepository;
        this.cryptographyService = cryptographyService;
        this.tokenService = tokenService;
    }
    public async Task<Result<AdmLoginCommandResponse>> Handle(AdmLoginCommand request, CancellationToken cancellationToken)
    {
        var adm = await userRepository.GetByEmail(request.Email.ToLower(), cancellationToken);
        if (adm is null)
            return Result.Fail(new ApplicationError("Incorrect email/password"));

        if (!adm.isActive)
            return Result.Fail(new ApplicationError("Incorrect email/password"));

        if (!cryptographyService.Compare(adm.Hash, request.Password, adm.Salt))
            return Result.Fail(new ApplicationError("Incorrect email/password"));

        var token = tokenService.GenerateToken(adm);

        return Result.Ok(new AdmLoginCommandResponse(adm.Name, adm.Email, token));
    }
}