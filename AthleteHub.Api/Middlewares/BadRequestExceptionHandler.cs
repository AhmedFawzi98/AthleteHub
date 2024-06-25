using AthleteHub.Api.Dtos;
using AthleteHub.Domain.Exceptions;
using Azure;
using Microsoft.AspNetCore.Diagnostics;

namespace AthleteHub.Api.Middlewares;

public class BadRequestExceptionHandler(ILogger<BadRequestExceptionHandler> _logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not BadRequestException badRequestException)
            return false;


        var response = httpContext.Response;

        response.StatusCode = StatusCodes.Status400BadRequest;
        response.ContentType = "application/json";

        var responerrorResponseDtoseDto = new ErrorDto()
        {
            Message = exception.Message,
            Errors = badRequestException.Errors.GroupBy(e => e.Field)
                                  .ToDictionary(g => g.Key, g => g.Select(e => e.Message).ToArray())
        };
            
        await response.WriteAsJsonAsync(responerrorResponseDtoseDto).ConfigureAwait(false);

        _logger.LogError(badRequestException, "Error Details : {@errorResponseDto}", responerrorResponseDtoseDto);
        return true;
    }
}
