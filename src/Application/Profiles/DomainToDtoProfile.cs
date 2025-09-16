using Application.DTOs.Eventos;
using Application.DTOs.Noticias;
using Application.DTOs.Ranking;
using AutoMapper;
using Domain.Entities.Eventos;
using Domain.Entities.Noticia;

namespace Application.Profiles;

public class DomainToDtoProfile : Profile
{
    public DomainToDtoProfile()
    {
        CreateMap<Evento, EventoDto>();
        CreateMap<Noticia, NoticiaDto>();
        CreateMap<(Guid UsuarioMcId, int Vitorias), ItemRankingDto>()
            .ForMember(d => d.UsuarioMcId, o => o.MapFrom(s => s.UsuarioMcId))
            .ForMember(d => d.Vitorias, o => o.MapFrom(s => s.Vitorias));
    }
}
