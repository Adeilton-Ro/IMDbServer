using FluentResults;
using IMDb.Application.Extension;
using IMDb.Application.Services.Crypto;
using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using MediatR;

namespace IMDb.Application.Features.AdmEdit;
public class EditAdmCommandHandler : IRequestHandler<EditAdmCommand, Result>
{
    private readonly IUserRepository<Adm> userRepository;
    private readonly ICryptographyService cryptographyService;
    private readonly IUnitOfWork unitOfWork;

    public EditAdmCommandHandler(IUserRepository<Adm> userRepository, ICryptographyService cryptographyService, IUnitOfWork unitOfWork)
    {
        this.userRepository = userRepository;
        this.cryptographyService = cryptographyService;
        this.unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(EditAdmCommand request, CancellationToken cancellationToken)
    {
        var adm = await userRepository.GetById(request.Id, cancellationToken);
        if (adm is null)
            return Result.Fail(new ApplicationError("User was not find"));

        if (adm.Email != request.Email && !await userRepository.IsUniqueEmail(request.Email, cancellationToken))
            return Result.Fail(new ApplicationError("this email is already in use"));

        var salt = cryptographyService.CreateSalt();
        adm.Name = request.Name;
        adm.Email = request.Email;
        adm.Salt = salt;
        adm.Hash = cryptographyService.Hash(request.Password, salt);

        userRepository.Edit(adm);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}