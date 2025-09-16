using Api.Extensions.DI;
using Api.Extensions;
using Serilog;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Logging
builder.Host.UseSerilog((ctx, lc) => lc
    .ReadFrom.Configuration(ctx.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console());

// Central DI setup
builder.Services.AddRadarDependencyInjection(builder.Configuration, builder.Environment);

var app = builder.Build();

// Apply EF Core migrations only in Development or when debugging
var shouldAutoMigrate = builder.Environment.IsDevelopment() || Debugger.IsAttached;
if (shouldAutoMigrate)
{
    using var scope = app.Services.CreateScope();
    var sp = scope.ServiceProvider;
    var logger = sp.GetRequiredService<ILoggerFactory>().CreateLogger("Startup");
    try
    {
        var db = sp.GetRequiredService<RadarDbContext>();
        if (db.Database.IsRelational())
        {
            db.Database.Migrate();
            logger.LogInformation("Database migrated successfully.");
        }
        else
        {
            db.Database.EnsureCreated();
            logger.LogInformation("Database ensured created (non-relational provider).");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error applying database migrations at startup.");
        throw;
    }
}

// Pipeline
app.UseRadarPipeline(builder.Environment);

app.Run();

public partial class Program { }
