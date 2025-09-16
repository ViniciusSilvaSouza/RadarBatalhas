using Domain.Enums;

namespace Application.DTOs.Eventos;

public class EventoDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public string NomeLocal { get; set; } = string.Empty;
    public StatusEvento IdStatus { get; set; }
    public string? ImagemUrl { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}
