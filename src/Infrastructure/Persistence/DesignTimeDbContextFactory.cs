using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<RadarDbContext>
{
    public RadarDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RadarDbContext>();
        var host = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
        var port = Environment.GetEnvironmentVariable("DB_PORT") ?? "3306";
        var db   = Environment.GetEnvironmentVariable("DB_NAME") ?? "radar";
        var user = Environment.GetEnvironmentVariable("DB_USER") ?? "root";
        var pwd  = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "root";
        var cs = $"Server={host};Port={port};Database={db};User={user};Password={pwd};";

        var serverVersion = ServerVersion.Parse("8.0.36");
        optionsBuilder.UseMySql(cs, serverVersion, b => b.MigrationsAssembly("Infrastructure"));
        return new RadarDbContext(optionsBuilder.Options);
    }
}
