namespace IMDb.Application.Services.Crypto;
public interface ICryptographyService
{
    string Hash(string plainText, string salt);
    public string CreateSalt();
}