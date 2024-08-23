using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace ComicsShop.Identity.Api.Middlewares;

public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            logger.LogInformation("Handling request: {Method} {Path}", context.Request.Method, context.Request.Path);

            await next(context);

            logger.LogInformation("Finished handling request.");
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "An error occurred while handling the request.");

            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "An error occurred while processing your request.",
                Detail = ex.Message,
                Instance = context.Request.Path
            };

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(json);
        }
    }
}