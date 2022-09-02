using FluentResults;
using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using MediatR;

namespace IMDb.Application.Features.Account.Clients.Disable;
public class DisableClientAccountCommandHandler : IRequestHandler<DisableClientAccountCommand, Result>
{
    private readonly IUserRepository<Client> userRepository;
    private readonly IUnitOfWork unitOfWork;

    public DisableClientAccountCommandHandler(IUserRepository<Client> userRepository, IUnitOfWork unitOfWork)
    {
        this.userRepository = userRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DisableClientAccountCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
