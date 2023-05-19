using Charisma.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Charisma.Application.Common.Middlewares;


public class ExceptionHandler
{
    private readonly RequestDelegate _next;

    private readonly ILogger<ExceptionHandler> _logger;

    public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        string response;

        if (exception is CharismaBaseException)
        {
            context.Response.StatusCode = 400;
            response = "Bad Request: Logic not considered!";
        }
        else if (exception is AuthenticationException)
        {
            context.Response.StatusCode = 401;
            response = "UnAthenticated";
        }
        else
        {
            context.Response.StatusCode = 500;
            response = $"Somthing went wrong";
        }

        LogException(context, exception);

        return context.Response.WriteAsync(response);
    }

    private void LogException(HttpContext context, Exception exception)
    {
        string username = context.User.Identity.Name;
        string userId = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        string logMessage = $"Request - {context.Request?.Method}: {context.Request?.Path.Value} - " +
                                        "{ExceptionMessage} - {StatusCode} - {Username} ({UserId})";

        _logger.LogError(exception,
                         logMessage,
                         exception.Message,
                         context.Response.StatusCode,
                         username,
                         userId);
    }
}
