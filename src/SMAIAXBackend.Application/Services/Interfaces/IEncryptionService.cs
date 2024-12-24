namespace SMAIAXBackend.Application.Services.Interfaces;

public interface IEncryptionService
{
    (string PublicKey, string PrivateKey) GenerateKeys();
}