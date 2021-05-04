using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using MyStagram.Core.Services.Interfaces;

namespace MyStagram.Core.Services
{

    public class CryptoService : ICryptoService
    {
        private readonly IDataProtectionProvider dataProtectionProvider;

        public string ProtectorToken { get; }

        public IConfiguration Configuration { get; }

        public CryptoService(IDataProtectionProvider dataProtectionProvider, IConfiguration configuration)
        {
            this.dataProtectionProvider = dataProtectionProvider;

            Configuration = configuration;
            ProtectorToken = Configuration.GetValue<string>("Constants:Token");
        }

        public string Encrypt(string plainText)
        {
            var dataProtector = dataProtectionProvider.CreateProtector(ProtectorToken);

            return dataProtector.Protect(plainText);
        }

        public string Decrypt(string cipherText)
        {
            try
            {
                var dataProtector = dataProtectionProvider.CreateProtector(ProtectorToken);

                return dataProtector.Unprotect(cipherText);
            }
            catch (CryptographicException)
            {
                throw new Exception();
            }
        }
    }
}