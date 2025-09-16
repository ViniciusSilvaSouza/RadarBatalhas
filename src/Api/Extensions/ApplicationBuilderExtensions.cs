using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Api.Middlewares;
using Serilog;
using Serilog.AspNetCore;

namespace Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseRadarPipeline(this IApplicationBuilder app, IHostEnvironment env)
    {
        app.UseMiddleware<CorrelationIdMiddleware>();
        app.UseMiddleware<ProblemDetailsMiddleware>();

        app.UseSerilogRequestLogging();
        app.UseRouting();

        app.UseSwagger();
        app.UseSwaggerUI();

        // Static files for uploads (wwwroot/uploads)
        app.UseStaticFiles();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks("/health");
            endpoints.MapControllers();
        });

        return app;
    }
}
