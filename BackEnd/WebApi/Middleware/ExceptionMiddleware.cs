using System.Net;
using Core.Exceptions;

namespace WebApi.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (NotFoundException ex)
        {
            await HandleExceptionAsync(httpContext, HttpStatusCode.NotFound, ex);
        }
        catch (UnauthorizedException ex)
        {
            await HandleExceptionAsync(httpContext, HttpStatusCode.Unauthorized, ex);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsync($"Path: {context.Request.Path}. {ex.Message}");
    }
}