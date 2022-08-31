using IMDb.Domain.Entities;

namespace IMDb.Application.Services.Token;
public interface ITokenService
{
    string GenerateToken(Client client);
    string GenerateToken(Adm adm);
}