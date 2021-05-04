using Microsoft.Extensions.Configuration;

namespace MyStagram.Core.Services.Interfaces
{
    public interface ICryptoService
    {
        string ProtectorToken { get; }
        IConfiguration Configuration { get; }

        string Decrypt(string cipherText);
        string Encrypt(string plainText);
    }
}