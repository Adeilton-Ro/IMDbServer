using FluentResults;
using IMDb.Application.Extension;
using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using MediatR;

namespace IMDb.Application.Features.Account.Adms.Disable;
public class DisableAdmAccountCommandHandler : IRequestHandler<DisableAdmAccountCommand, Result>
{
    private readonly IUserRepository<Adm> userRepository;
    private readonly IUnitOfWork unitOfWork;

    public DisableAdmAccountCommandHandler(IUserRepository<Adm> userRepository, IUnitOfWork unitOfWork)
    {
        this.userRepository = userRepository;
        this.unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(DisableAdmAccountCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}