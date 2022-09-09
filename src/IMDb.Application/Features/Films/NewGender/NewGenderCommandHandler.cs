using FluentResults;
using IMDb.Domain.Entities;
using IMDb.Infra.Database.Abstraction.Interfaces;
using IMDb.Infra.Database.Abstraction.Interfaces.Repositories;
using MediatR;

namespace IMDb.Application.Features.Films.NewGender;
public class NewGenderCommandHandler : IRequestHandler<NewGenderCommand, Result<NewGenderCommandResponse>>
{
    private readonly IGenderRepository genderRepository;
    private readonly IUnitOfWork unitOfWork;

    public NewGenderCommandHandler(IGenderRepository genderRepository, IUnitOfWork unitOfWork)
    {
        this.genderRepository = genderRepository;
        this.unitOfWork = unitOfWork;
    }
    public async Task<Result<NewGenderCommandResponse>> Handle(NewGenderCommand request, CancellationToken cancellationToken)
    {
        var gender = new Gender
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };

        await genderRepository.Create(gender, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(new NewGenderCommandResponse(gender.Id));
    }
}