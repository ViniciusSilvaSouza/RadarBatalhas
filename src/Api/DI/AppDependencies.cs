using Application.UseCases.Evento;
using Domain.Compartilhado.Contracts;
using Domain.Repositorios.Eventos;
using Domain.Repositorios.Ranking;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Radar.Infrastructure.Repositories;

namespace Api.DI;

public static class AppDependencies
{
    public static IServiceCollection AddRadarAppDependencies(this IServiceCollection services)
    {
        // UoW
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Repositories
        services.AddScoped<IEventoRepositorio, EventoRepository>();
        services.AddScoped<IRankingLeituraRepositorio, RankingLeituraRepository>();
        services.AddScoped<IInscricaoRepositorio, InscricaoRepository>();

        // UseCases
        services.AddScoped<CriarEventoUseCase>();
        services.AddScoped<ListarProximosEventosUseCase>();
        services.AddScoped<ObterEventoPorIdUseCase>();

        return services;
    }
}
