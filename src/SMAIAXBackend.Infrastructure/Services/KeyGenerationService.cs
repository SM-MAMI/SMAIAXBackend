using SMAIAXBackend.Application.Services.Interfaces;

namespace SMAIAXBackend.Infrastructure.Services;

public class KeyGenerationService : IKeyGenerationService
{
    public (string PublicKey, string PrivateKey) GenerateKeys()
    {
        using var rsa = new System.Security.Cryptography.RSACryptoServiceProvider(4096);
        string publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
        string privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
        return (publicKey, privateKey);
    }
}