using Domain.Entities.Noticia;

namespace Application.DTOs.Noticias;

public class NoticiaDto
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Conteudo { get; set; } = string.Empty;
    public EstadoNoticia Estado { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime? PublicadoEm { get; set; }
}
