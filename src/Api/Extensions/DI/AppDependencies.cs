using Application.UseCases.Evento;
using Application.UseCases.Usuarios;
using Domain.Compartilhado.Contracts;
using Domain.Repositorios.Eventos;
using Domain.Repositorios.Ranking;
using Domain.Repositorios.Usuarios;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Radar.Infrastructure.Repositories;
using Application.Contracts;
using Infrastructure.Services;

namespace Api.Extensions.DI;

public static class AppDependencies
{
    public static IServiceCollection AddRadarAppDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IEventoRepositorio, EventoRepository>();
        services.AddScoped<IRankingLeituraRepositorio, RankingLeituraRepository>();
        services.AddScoped<IInscricaoRepositorio, InscricaoRepository>();
        services.AddScoped<IUsuariosRepositorio, UsuariosRepository>();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddScoped<CriarEventoUseCase>();
        services.AddScoped<ListarProximosEventosUseCase>();
        services.AddScoped<ObterEventoPorIdUseCase>();
        services.AddScoped<ListarEventosDoOrganizadorUseCase>();

        services.AddScoped<CriarOuAtualizarUsuarioAdminUseCase>();
        services.AddScoped<SelfOnboardUseCase>();

        return services;
    }
}
