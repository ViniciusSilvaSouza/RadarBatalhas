using System;

namespace Domain.Entities.Eventos;

public class VitoriaMcEvento
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UsuarioMcId { get; set; }
    public Guid EventoId { get; set; }
    public DateTime DataVitoria { get; set; } = DateTime.UtcNow;
}
