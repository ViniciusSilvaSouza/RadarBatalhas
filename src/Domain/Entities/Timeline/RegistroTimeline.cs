using System;

namespace Domain.Entities.Timeline;

public class RegistroTimeline
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid? EventoId { get; set; }
    public Guid? UsuarioId { get; set; }
    public string Tipo { get; set; } = string.Empty; // e.g., VITORIA_EVENTO, WALKOVER_ATRIBUIDO
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public string Metadados { get; set; } = string.Empty; // JSON
}
