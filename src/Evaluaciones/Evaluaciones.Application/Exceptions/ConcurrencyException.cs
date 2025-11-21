namespace Evaluaciones.Application.Exceptions;

public sealed class ConcurrencyException(string message, Exception exception) : Exception(message, exception) {}