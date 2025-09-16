using Domain.Enums;
using System;

namespace Domain.Entities.Eventos;

public class Evento
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public string NomeLocal { get; set; } = string.Empty;
    public bool EhRecorrente { get; set; }
    public Guid? SerieEventoId { get; set; }
    public int? MaximoParticipantes { get; set; }
    public int? QuantidadeVagasFixas { get; set; }
    public int? QuantidadeVagasSorteio { get; set; }
    public StatusEvento IdStatus { get; set; } = StatusEvento.RASCUNHO;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public Guid OrganizadorId { get; set; }

    // Exibição no frontend
    public string? ImagemUrl { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}
