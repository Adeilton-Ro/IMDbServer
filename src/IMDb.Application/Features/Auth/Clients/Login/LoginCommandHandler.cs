using FluentResults;
using IMDb.Application.Extension;
using IMDb.Application.Services.Crypto;
using IMDb.Application.Services.Token;
using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using MediatR;
using System.Runtime.Intrinsics.Arm;

namespace IMDb.Application.Features.Auth.Clients.Login;
public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginCommandResponse>>
{
    private readonly IUserRepository<Client> userRepository;
    private readonly ICryptographyService cryptographyService;
    private readonly ITokenService tokenService;

    public LoginCommandHandler(IUserRepository<Client> userRepository, ICryptographyService cryptographyService, ITokenService tokenService)
    {
        this.userRepository = userRepository;
        this.cryptographyService = cryptographyService;
        this.tokenService = tokenService;
    }

    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var client = await userRepository.GetByEmail(request.Email.ToLower(), cancellationToken);
        if (client is null)
            return Result.Fail(new ApplicationError("Incorrect email/password"));

        if (!client.isActive)
            return Result.Fail(new ApplicationError("Incorrect email/password"));

        if (!cryptographyService.Compare(client.Hash, request.Password, client.Salt))
            return Result.Fail(new ApplicationError("Incorrect email/password"));

        var token = tokenService.GenerateToken(client);

        return Result.Ok(new LoginCommandResponse(client.Name, client.Email, token));
    }
}