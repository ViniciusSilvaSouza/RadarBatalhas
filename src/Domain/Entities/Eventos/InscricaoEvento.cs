using System;

namespace Domain.Entities.Eventos;

public class InscricaoEvento
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid EventoId { get; set; }
    public Guid UsuarioMcId { get; set; }
    public DateTime InscritoEm { get; set; } = DateTime.UtcNow;
}
