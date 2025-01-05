namespace SMAIAXBackend.Application.Exceptions;

public class PolicyNotFoundException(Guid policyId) : Exception
{
    public override string Message { get; } = $"Policy with id '{policyId}' not found.";
}