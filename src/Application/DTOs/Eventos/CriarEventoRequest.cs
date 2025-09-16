using System;

namespace Application.DTOs.Eventos;

public class CriarEventoRequest
{
    public string Nome { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public string NomeLocal { get; set; } = string.Empty;
    public string? ImagemUrl { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}
