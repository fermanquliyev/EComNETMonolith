﻿using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EComNetMonolith.Shared.Exceptions.Handler;

public class GlobalExceptionHandler
    (ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, exception.Message);

        (string Detail, string Title, int StatusCode) problem = exception switch
        {
            InternalServerException => (exception.Message, exception.GetType().Name, StatusCodes.Status500InternalServerError),
            ValidationException => (exception.Message, exception.GetType().Name, StatusCodes.Status400BadRequest),
            BadRequestException => (exception.Message, exception.GetType().Name, StatusCodes.Status400BadRequest),
            NotFoundException => (exception.Message, exception.GetType().Name, StatusCodes.Status404NotFound),
            _ => (
                exception.Message,
                exception.GetType().Name,
                StatusCodes.Status500InternalServerError
            )
        };

        var problemDetails = new ProblemDetails
        {
            Title = problem.Title,
            Status = problem.StatusCode,
            Detail = problem.Detail,
            Instance = httpContext.Request.Path
        };

        problemDetails.Extensions.Add("TraceId", httpContext.TraceIdentifier);

        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
        }

        httpContext.Response.StatusCode = problem.StatusCode;
        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}
