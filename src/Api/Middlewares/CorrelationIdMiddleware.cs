using System.Diagnostics;

namespace Api.Middlewares;

public class CorrelationIdMiddleware(RequestDelegate next)
{
    private const string HeaderName = "X-Correlation-Id";

    public async Task Invoke(HttpContext context)
    {
        var correlationId = context.Request.Headers.TryGetValue(HeaderName, out Microsoft.Extensions.Primitives.StringValues value)
            ? value.ToString()
            : Guid.NewGuid().ToString();
        context.Response.Headers[HeaderName] = correlationId;
        using var _ = new Activity("request").Start();
        await next(context);
    }
}
