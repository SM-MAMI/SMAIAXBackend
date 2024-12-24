using SMAIAXBackend.Application.Services.Interfaces;

namespace SMAIAXBackend.Infrastructure.Services;

public class EncryptionService : IEncryptionService
{
    public (string PublicKey, string PrivateKey) GenerateKeys()
    {
        using var rsa = new System.Security.Cryptography.RSACryptoServiceProvider(4096);
        string publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
        string privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
        return (publicKey, privateKey);
    }
    
    public string Encrypt(string plainText, string publicKey)
    {
        using var rsa = new System.Security.Cryptography.RSACryptoServiceProvider();
        rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);
        var encryptedData = rsa.Encrypt(System.Text.Encoding.UTF8.GetBytes(plainText), false);
        return Convert.ToBase64String(encryptedData);
    }
}