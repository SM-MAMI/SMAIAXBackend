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
            using var rsa = RSA.Create(4096);
            string publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
            string privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
            return (publicKey, privateKey);
        }

        public string Encrypt(string data, string publicKey)
        {
            RSACryptoServiceProvider rsaCsp = new RSACryptoServiceProvider(4069);
            StringReader sr = new System.IO.StringReader(publicKey);
            XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            var deserializedSr = xs.Deserialize(sr);
            RSAParameters pubKey;
            if (deserializedSr != null)
            {
                pubKey = (RSAParameters)deserializedSr;
            }
            else
            {
                throw new DeserializationException("Could not deserialize public key", null);
            }

            rsaCsp.ImportParameters(pubKey);

            UnicodeEncoding byteConverter = new UnicodeEncoding();

            byte[] dataToEncrypt = byteConverter.GetBytes(data);

            byte[] encryptedData = rsaCsp.Encrypt(dataToEncrypt, true);

            return Convert.ToBase64String(encryptedData);
        }
    }
}