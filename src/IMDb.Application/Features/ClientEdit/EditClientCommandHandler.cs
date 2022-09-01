using FluentResults;
using IMDb.Application.Extension;
using IMDb.Application.Services.Crypto;
using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using MediatR;

namespace IMDb.Application.Features.ClientEdit;
public class EditClientCommandHandler : IRequestHandler<EditClientCommand, Result>
{
    private readonly IUserRepository<Client> userRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly ICryptographyService cryptographyService;

    public EditClientCommandHandler(IUserRepository<Client> userRepository, IUnitOfWork unitOfWork, ICryptographyService cryptographyService)
    {
        this.userRepository = userRepository;
        this.unitOfWork = unitOfWork;
        this.cryptographyService = cryptographyService;
    }

    public async Task<Result> Handle(EditClientCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}