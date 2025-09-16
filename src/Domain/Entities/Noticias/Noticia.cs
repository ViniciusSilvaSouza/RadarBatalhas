using System;

namespace Domain.Entities.Noticia;

public enum EstadoNoticia { RASCUNHO = 1, PUBLICADA = 2 }

public class Noticia
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Titulo { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Conteudo { get; set; } = string.Empty;
    public EstadoNoticia Estado { get; set; } = EstadoNoticia.RASCUNHO;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime? PublicadoEm { get; set; }
}
