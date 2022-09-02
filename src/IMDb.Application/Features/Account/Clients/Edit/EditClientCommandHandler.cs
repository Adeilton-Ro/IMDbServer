using FluentResults;
using IMDb.Application.Extension;
using IMDb.Application.Services.Crypto;
using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using MediatR;

namespace IMDb.Application.Features.Account.Clients.Edit;
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
        var client = await userRepository.GetById(request.Id, cancellationToken);
        if (client is null)
            return Result.Fail(new ApplicationError("User was not find"));

        if (client.Email != request.Email && !await userRepository.IsUniqueEmail(request.Email, cancellationToken))
            return Result.Fail(new ApplicationError("this email is already in use"));

        var salt = cryptographyService.CreateSalt();
        client.Name = request.Name;
        client.Email = request.Email;
        client.Salt = salt;
        client.Hash = cryptographyService.Hash(request.Password, salt);

        userRepository.Edit(client);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}