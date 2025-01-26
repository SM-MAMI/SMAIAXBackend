namespace SMAIAXBackend.Application.Exceptions;

public class ContractNotFoundException(Guid contractId) : Exception
{
    public override string Message { get; } = $"Contract with id '{contractId} not found.";
}