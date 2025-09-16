using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Application.DTOs.Eventos;

namespace Integration;

public class CriarEventoERankingTests
{
    private class TestFactory : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment("Test");
            return base.CreateHost(builder);
        }
    }

    [Fact]
    public async Task PostCriarEventoDepoisGetRankingVazio()
    {
        await using var factory = new TestFactory();
        var client = factory.CreateClient();

        var req = new CriarEventoRequest
        {
            Nome = "Copa Radar",
            NomeLocal = "Praça Central",
            Data = DateTime.UtcNow.AddDays(10)
        };

        var resp = await client.PostAsJsonAsync("/api/v1/admin/eventos", req);
        Assert.Equal(HttpStatusCode.OK, resp.StatusCode);

        var ranking = await client.GetAsync("/api/v1/public/ranking");
        Assert.Equal(HttpStatusCode.OK, ranking.StatusCode);
    }
}
