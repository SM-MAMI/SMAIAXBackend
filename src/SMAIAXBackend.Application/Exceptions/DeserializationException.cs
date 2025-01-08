namespace SMAIAXBackend.Application.Exceptions;

public class DeserializationException(string? message, Exception? innerException) : Exception(message, innerException);