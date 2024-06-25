using AthleteHub.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace AthleteHub.Api.Middlewares;

public class UnAuthorizedExceptionHandler(ILogger<UnAuthorizedExceptionHandler> _logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var response = httpContext.Response;
        if (exception is not UnAuthorizedException unAuthorizedException)
            return false;
        
        response.StatusCode = StatusCodes.Status401Unauthorized;
        response.ContentType = "text/plain";
        await response.WriteAsync(unAuthorizedException.Message).ConfigureAwait(false);
        _logger.LogError(unAuthorizedException, unAuthorizedException.Message);
        return true;        
    }
}
