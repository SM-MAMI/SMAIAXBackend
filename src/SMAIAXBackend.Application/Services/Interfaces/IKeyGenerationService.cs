namespace SMAIAXBackend.Application.Services.Interfaces;

public interface IKeyGenerationService
{
    (string PublicKey, string PrivateKey) GenerateKeys();
}