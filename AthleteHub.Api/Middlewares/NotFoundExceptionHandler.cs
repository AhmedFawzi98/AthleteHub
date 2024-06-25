using AthleteHub.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace AthleteHub.Api.Middlewares;

public class NotFoundExceptionHandler(ILogger<NotFoundExceptionHandler> _logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var response = httpContext.Response;
        if (exception is not NotFoundException notFoundEx)
            return false;
        
        response.StatusCode = StatusCodes.Status404NotFound;
        response.ContentType = "text/plain";
        await response.WriteAsync(notFoundEx.Message).ConfigureAwait(false);
        _logger.LogError(notFoundEx,notFoundEx.Message);
        return true;        
    }
}
