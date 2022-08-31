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
        throw new NotImplementedException();
    }
}
