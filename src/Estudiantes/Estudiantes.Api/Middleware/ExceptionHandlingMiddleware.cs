using Microsoft.AspNetCore.Mvc;
using Estudiantes.Application.Exceptions;

namespace Estudiantes.Api.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrio una excepcion: {Exception}", ex);
            var excepcionDetail = GetExceptionDeails(ex);
            var problemDetail = new ProblemDetails
            {
                Status = excepcionDetail.Status,
                Type = excepcionDetail.Type,
                Title = excepcionDetail.Title,
                Detail = excepcionDetail.Detail,
            };

            if (excepcionDetail.Errors is not null)
            {
                problemDetail.Extensions["errors"] = excepcionDetail.Errors;
            }

            context.Response.StatusCode = excepcionDetail.Status;

            await context.Response.WriteAsJsonAsync(problemDetail);
        }
    }

    private static ExceptionDetails GetExceptionDeails(Exception exception)
    {
        return exception switch
        {
            ValidationExceptions validationException => new ExceptionDetails(
                StatusCodes.Status400BadRequest,
                "ValidationFailure",
                "Validacion de Error",
                "Han ocurrido uno o mas errores de validacion",
                validationException.Errors
            ),
            _ => new ExceptionDetails(
                  StatusCodes.Status500InternalServerError,
                 "ServerError",
                 "Error de Servidor",
                 "Han ocurrido un inesperado error en la app",
                 null
                )
        };
    }

    internal record ExceptionDetails
    (
        int Status,
        string Type,
        string Title,
        string Detail,
        IEnumerable<object>? Errors
    );
}