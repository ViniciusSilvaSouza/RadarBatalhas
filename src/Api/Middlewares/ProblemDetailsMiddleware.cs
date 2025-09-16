using System.Net;
using System.Text.Json;
using Application.DTOs.Common;

namespace Api.Middlewares;

public class ProblemDetailsMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try { await next(context); }
        catch (UnauthorizedAccessException ex)
        {
            await WriteAsync(context, (int)HttpStatusCode.Forbidden, "forbidden", ex.Message);
        }
        catch (Exception ex)
        {
            await WriteAsync(context, (int)HttpStatusCode.InternalServerError, "error", ex.Message);
        }
    }

    private static async Task WriteAsync(HttpContext ctx, int status, string code, string message)
    {
        var corr = ctx.Response.Headers.TryGetValue("X-Correlation-Id", out var c) ? c.ToString() : string.Empty;
        var pd = new ErrorDetails(status, code, message, corr);
        ctx.Response.StatusCode = status;
        ctx.Response.ContentType = "application/json";
        await ctx.Response.WriteAsync(JsonSerializer.Serialize(pd));
    }
}
