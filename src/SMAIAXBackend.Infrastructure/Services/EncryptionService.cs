using System.Security.Cryptography;
using System.Text;

using SMAIAXBackend.Application.Services.Interfaces;

namespace SMAIAXBackend.Infrastructure.Services
{
    public class EncryptionService : IEncryptionService
    {
        public (string PublicKey, string PrivateKey) GenerateKeys()
        {
            using var rsa = RSA.Create(4096);
            string publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
            string privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
            return (publicKey, privateKey);
        }

        public string Encrypt(string data, string publicKey)
        {
            using var rsa = RSA.Create();
            rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);

            var encryptedData = rsa.Encrypt(Encoding.UTF8.GetBytes(data), RSAEncryptionPadding.OaepSHA256);
            return Convert.ToBase64String(encryptedData);
        }
    }
}