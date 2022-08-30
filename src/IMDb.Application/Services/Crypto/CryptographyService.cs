using System.Security.Cryptography;
using System.Text;

namespace IMDb.Application.Services.Crypto;
public class CryptographyService : ICryptographyService
{
    public string CreateSalt()
    {
        var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
        var buff = new byte[10];
        rng.GetBytes(buff);
        return Convert.ToBase64String(buff);
    }

    public string Hash(string plainText, string salt)
    {
        using (var sha256Hash = SHA256.Create())
        {
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(plainText + salt));

            var builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
