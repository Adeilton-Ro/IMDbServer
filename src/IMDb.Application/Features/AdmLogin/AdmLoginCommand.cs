using FluentResults;
using MediatR;

namespace IMDb.Application.Features.AdmLogin;
public record AdmLoginCommand(string Email, string Password) : IRequest<Result<AdmLoginCommandResponse>>;