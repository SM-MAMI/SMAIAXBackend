using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

using SMAIAXBackend.Application.Exceptions;
using SMAIAXBackend.Application.Services.Interfaces;

namespace SMAIAXBackend.Infrastructure.Services
{
    public class EncryptionService : IEncryptionService
    {
        public (string PublicKey, string PrivateKey) GenerateKeys()
        {
            using var rsaCsp = new RSACryptoServiceProvider(4096);
            try
            {
                string publicKey = rsaCsp.ToXmlString(false);
                string privateKey = rsaCsp.ToXmlString(true);
                return (publicKey, privateKey);
            }
            finally
            {
                rsaCsp.PersistKeyInCsp = false;
            }
        }


        public string Encrypt(string data, string publicKey)
        {
            using var rsaCsp = new RSACryptoServiceProvider(4096);

            rsaCsp.FromXmlString(publicKey);

            byte[] dataToEncrypt = Encoding.UTF8.GetBytes(data);

            byte[] encryptedData = rsaCsp.Encrypt(dataToEncrypt, true);

            return Convert.ToBase64String(encryptedData);
        }
    }
}