using FluentResults;
using IMDb.Application.Extension;
using IMDb.Application.Services.Crypto;
using IMDb.Application.Services.Token;
using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using MediatR;

namespace IMDb.Application.Features.Login;
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
        throw new NotImplementedException();
    }
}