using IMDb.Domain.Entities.Abstract;

namespace IMDb.Application.Services.Token;
public interface ITokenService
{
    string GenerateToken(User client);
}