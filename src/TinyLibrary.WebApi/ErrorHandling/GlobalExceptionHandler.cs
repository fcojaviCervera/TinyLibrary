using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TinyLibrary.Application.Exceptions;
using TinyLibrary.Domain.Exceptions;

namespace TinyLibrary.WebApi.ErrorHandling
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var (statusCode, title) = exception switch
            {
                NotFoundException => (StatusCodes.Status404NotFound, "Recurso no encontrado"),
                DomainException => (StatusCodes.Status409Conflict, "Regla de negocio violada"),
                DuplicateIsbnException => (StatusCodes.Status409Conflict, "ISBN duplicado"),
                DuplicateEmailMemberException => (StatusCodes.Status409Conflict, "Email de miembro duplicado"),
                _ => (StatusCodes.Status500InternalServerError, "Ocurrió un error inesperado")
            };

            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = exception.Message
            }, cancellationToken);

            return true;
        }
    }
}
