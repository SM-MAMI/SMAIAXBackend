namespace SMAIAXBackend.Application.Exceptions;

public class UserIdNotFoundException : Exception
{
    public override string Message { get; } = "No user id found in claims principal.";
}