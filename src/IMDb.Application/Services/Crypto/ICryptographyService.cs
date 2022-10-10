namespace IMDb.Application.Services.Crypto;
public interface ICryptographyService
{
    string Hash(string plainText, string salt);
    string CreateSalt();
    bool Compare(string cryptoPlainText, string plainText, string salt);
}